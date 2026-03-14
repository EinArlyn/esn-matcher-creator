using ESNMatcherCreator.Helpers;
using ESNMatcherCreator.Models;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace ESNMatcherCreator.ViewModels
{
    /// <summary>
    /// Класс модели представления
    /// </summary>
    public class MainViewModel : NotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<MatcherModel>? matchers;
        private bool isFormatButtonEnabled;
        private bool isCopyButtonEnabled;
        private bool isSaveButtonEnabled;
        private bool isActiveSnackBar;
        private string formattedData = string.Empty;
        private string contentSnackBar = string.Empty;
        #endregion

        #region Properties
        /// <summary>
        /// Список тикеров для матчинга
        /// </summary>
        public ObservableCollection<MatcherModel>? MatcherDataList
        {
            get => matchers;
            set
            {
                matchers = value;
                OnPropertyChanged(nameof(MatcherDataList));
            }
        }

        /// <summary>
        /// Доступна кнопка форматирования данных
        /// </summary>
        public bool IsFormatButtonEnabled
        {
            get => isFormatButtonEnabled;
            set
            {
                isFormatButtonEnabled = value;
                OnPropertyChanged(nameof(IsFormatButtonEnabled));
            }
        }

        /// <summary>
        /// Доступна кнопка копирования в буфер
        /// </summary>
        public bool IsCopyButtonEnabled
        {
            get => isCopyButtonEnabled;
            set
            {
                isCopyButtonEnabled = value;
                OnPropertyChanged(nameof(IsCopyButtonEnabled));
            }
        }

        /// <summary>
        /// Доступна кнопка сохранения в файл
        /// </summary>
        public bool IsSaveButtonEnabled
        {
            get => isSaveButtonEnabled;
            set
            {
                isSaveButtonEnabled = value;
                OnPropertyChanged(nameof(IsSaveButtonEnabled));
            }
        }

        /// <summary>
        /// Активный SnackBar
        /// </summary>
        public bool IsActiveSnackBar
        {
            get => isActiveSnackBar;
            set
            {
                isActiveSnackBar = value;
                OnPropertyChanged(nameof(IsActiveSnackBar));
            }
        }

        /// <summary>
        /// Сообщение для SnackBar
        /// </summary>
        public string ContentSnackBar
        {
            get => contentSnackBar;
            set
            {
                contentSnackBar = value;
                OnPropertyChanged(nameof(ContentSnackBar));
            }
        }

        /// <summary>
        /// Форматированные данных
        /// </summary>
        public string FormattedData
        {
            get => formattedData;
            set
            {
                formattedData = value;
                OnPropertyChanged(nameof(FormattedData));
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Команда форматирования данных
        /// </summary>
        public RelayCommand FormatClick => new(obj =>
        {
            IsActiveSnackBar = false;

            ObservableCollection<MatcherModel> matcherList = new((ObservableCollection<MatcherModel>)obj);
            string formatData = "";
            string type = "";
            while(matcherList.Count > 0)
            {
                MatcherModel matcher = matcherList[0];
                ObservableCollection<MatcherModel> data = new(matcherList.Where(obj => obj.TickerId == matcher.TickerId && obj.Type == matcher.Type));
                if (type != matcher.Type)
                {
                    type = matcher.Type;
                    formatData += formatData.Length == 0 ? $"\t\t<!-- {type} -->" : $"\n\t\t<!-- {type} -->";
                }

                Formater formater = new(data);
                formatData += formatData.Length == 0 ? $"{formater.Get}" : $"\n{formater.Get}";
                matcherList = new(matcherList.Where(obj => obj.TickerId != data[0].TickerId));
            }

            FormattedData = formatData;
            IsCopyButtonEnabled = true;
            IsSaveButtonEnabled = true;

            ContentSnackBar = "Данные обработаны...";
            IsActiveSnackBar = true;
        });

        /// <summary>
        /// Команда копирования в буфер обмена форматированных данных
        /// </summary>
        public RelayCommand CopyClick => new(obj =>
        {
            IsActiveSnackBar = false;

            Clipboard.SetText(obj as string);

            ContentSnackBar = "Скопировано в буфер обмена...";
            IsActiveSnackBar = true;
        });

        /// <summary>
        /// Команда выхода из приложения
        /// </summary>
        public static RelayCommand ExitToAppClick => new(obj =>
        {
            Application.Current.Shutdown();
        });

        /// <summary>
        /// Команда для закрытия Snackbar
        /// </summary>
        public RelayCommand SnackbarCloseClick => new(obj =>
        {
            IsActiveSnackBar = false;
        });
        #endregion

        #region Methods
        /// <summary>
        /// Метод чтения CSV файла
        /// </summary>
        /// <param name="fileName">Полный путь к файлу</param>
        public void ReadCSVFile(string fileName)
        {
            IsActiveSnackBar = false;
            ClearEnabledButtons();
            string csvFileText = File.ReadAllText(fileName);
            csvFileText = csvFileText.Replace("\r\n", "\n");
            string[] matcherItem = csvFileText.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            MatcherDataList = new();

            foreach (var item in matcherItem)
            {
                string[] row = item.Split(';', StringSplitOptions.RemoveEmptyEntries);
                MatcherModel matcher = new()
                {
                    Type = row[0],
                    Ticker = row[1],
                    TickerId = Convert.ToInt32(row[2].ToString()),
                    LP = row[3],
                    LpId = Convert.ToInt32(row[4].ToString())
                };
                MatcherDataList.Add(matcher);
            }

            IsFormatButtonEnabled = MatcherDataList.Count != 0;

            ContentSnackBar = "CSV файл загружен...";
            IsActiveSnackBar = true;
        }

        /// <summary>
        /// Метод сохранения XML файла
        /// </summary>
        /// <param name="fileName">Полный путь к файлу</param>
        public void SaveXMLFile(string fileName)
        {
            IsActiveSnackBar = false;

            File.WriteAllText(fileName, FormattedData);

            ContentSnackBar = "XML файл сохранён...";
            IsActiveSnackBar = true;
        }

        /// <summary>
        /// Метод очистки доступности к кнопкам
        /// </summary>
        private void ClearEnabledButtons()
        {
            IsFormatButtonEnabled = false;
            IsCopyButtonEnabled = false;
            IsSaveButtonEnabled = false;
        }
        #endregion
    }
}
