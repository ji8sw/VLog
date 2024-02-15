using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VLog
{
    public class NoteEventArgs : EventArgs
    {
        public int Index { get; set; }

        public NoteEventArgs(int index)
        {
            Index = index;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Main_VehicleName.Content = Properties.Settings.Default.VehicleName;
            Main_BrandName.Content = Properties.Settings.Default.BrandName;

            ImportantInfo_VehicleNameInput.Text = Properties.Settings.Default.VehicleName;
            ImportantInfo_BrandNameInput.Text = Properties.Settings.Default.BrandName;

            ImportantInfo_VINInput.Text = Properties.Settings.Default.VIN;

            ImportantInfo_ManDatePt1.Text = Properties.Settings.Default.ManDate1.ToString();
            ImportantInfo_ManDatePt2.Text = Properties.Settings.Default.ManDate2.ToString();
            ImportantInfo_ManDatePt3.Text = Properties.Settings.Default.ManDate3.ToString();

            ImportantInfo_Odometer.Text = Properties.Settings.Default.OdometerKM.ToString();

            ImportantInfo_ServDatePt1.Text = Properties.Settings.Default.ServDate1.ToString();
            ImportantInfo_ServDatePt2.Text = Properties.Settings.Default.ServDate2.ToString();
            ImportantInfo_ServDatePt3.Text = Properties.Settings.Default.ServDate3.ToString();

            MainMenu.Visibility = Visibility.Visible;
            ImportantInfo.Visibility = Visibility.Collapsed;
            Notes.Visibility = Visibility.Collapsed;
            AddNote.Visibility = Visibility.Collapsed;
            OpenNoteView.Visibility = Visibility.Collapsed;

            if (Properties.Settings.Default.TitlesList == null)
                Properties.Settings.Default.TitlesList = new System.Collections.Specialized.StringCollection();

            if (Properties.Settings.Default.DescriptionsList == null)
                Properties.Settings.Default.DescriptionsList = new System.Collections.Specialized.StringCollection();

            for (int Index = 0; Index < Properties.Settings.Default.TitlesList.Count; Index++)
            {
                int capturedIndex = Index; 
                System.Windows.Controls.Button NewNoteButton = new System.Windows.Controls.Button();
                NewNoteButton.Content = Properties.Settings.Default.TitlesList[Index];
                NewNoteButton.Click += (sender, e) => OpenNote(sender, new NoteEventArgs(capturedIndex));
                ApplyButtonStyle(NewNoteButton);
                NotesCollection.Children.Add(NewNoteButton);

                Line NewLine = new Line();
                NewLine.Height = 10;
                NotesCollection.Children.Add(NewLine);
            }

            Properties.Settings.Default.Save();
        }

        public void GoToImportantInfo(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Collapsed;
            ImportantInfo.Visibility = Visibility.Visible;
            Notes.Visibility = Visibility.Collapsed;
            AddNote.Visibility = Visibility.Collapsed;
            OpenNoteView.Visibility = Visibility.Collapsed;

            ImportantInfo_VehicleNameInput.Text = Properties.Settings.Default.VehicleName;
            ImportantInfo_BrandNameInput.Text = Properties.Settings.Default.BrandName;
        }

        public void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Visible;
            ImportantInfo.Visibility = Visibility.Collapsed;
            Notes.Visibility = Visibility.Collapsed;
            AddNote.Visibility = Visibility.Collapsed;
            OpenNoteView.Visibility = Visibility.Collapsed;
        }

        public void GoToNotes(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Collapsed;
            ImportantInfo.Visibility = Visibility.Collapsed;
            Notes.Visibility = Visibility.Visible;
            AddNote.Visibility = Visibility.Collapsed;
            OpenNoteView.Visibility = Visibility.Collapsed;
        }

        public void GoToAddNote(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Collapsed;
            ImportantInfo.Visibility = Visibility.Collapsed;
            Notes.Visibility = Visibility.Collapsed;
            AddNote.Visibility = Visibility.Visible;
            OpenNoteView.Visibility = Visibility.Collapsed;
        }

        public void ImportantInfo_TextBoxesFinishedEditing(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.VehicleName = ImportantInfo_VehicleNameInput.Text;

            Properties.Settings.Default.BrandName = ImportantInfo_BrandNameInput.Text;

            Properties.Settings.Default.VIN = ImportantInfo_VINInput.Text;

            Properties.Settings.Default.ManDate1 = int.Parse(ImportantInfo_ManDatePt1.Text);
            Properties.Settings.Default.ManDate2 = int.Parse(ImportantInfo_ManDatePt2.Text);
            Properties.Settings.Default.ManDate3 = int.Parse(ImportantInfo_ManDatePt3.Text);

            Properties.Settings.Default.OdometerKM = int.Parse(ImportantInfo_Odometer.Text);

            Properties.Settings.Default.ServDate1 = int.Parse(ImportantInfo_ServDatePt1.Text);
            Properties.Settings.Default.ServDate2 = int.Parse(ImportantInfo_ServDatePt2.Text);
            Properties.Settings.Default.ServDate3 = int.Parse(ImportantInfo_ServDatePt3.Text);

            Properties.Settings.Default.Save();
            Main_VehicleName.Content = Properties.Settings.Default.VehicleName;
            Main_BrandName.Content = Properties.Settings.Default.BrandName;
        }

        public delegate void OpenNoteHandler(object sender, NoteEventArgs e);

        public void AddNote_Save(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TitlesList.Add(AddNote_Title.Text);
            Properties.Settings.Default.DescriptionsList.Add(AddNote_Desc.Text);

            System.Windows.Controls.Button NewNoteButton = new System.Windows.Controls.Button();
            NewNoteButton.Content = AddNote_Title.Text;
            NewNoteButton.Click += (sender, e) => OpenNote(sender, new NoteEventArgs(Properties.Settings.Default.TitlesList.Count - 1));
            ApplyButtonStyle(NewNoteButton);
            NotesCollection.Children.Add(NewNoteButton);

            Line NewLine = new Line();
            NewLine.Height = 10;
            NotesCollection.Children.Add(NewLine);

            Properties.Settings.Default.Save();

            GoToNotes(null, null);
        }

        public void OpenNote(object sender, NoteEventArgs e)
        {
            int Index = e.Index;

            MainMenu.Visibility = Visibility.Collapsed;
            ImportantInfo.Visibility = Visibility.Collapsed;
            Notes.Visibility = Visibility.Collapsed;
            AddNote.Visibility = Visibility.Collapsed;
            OpenNoteView.Visibility = Visibility.Visible;
            
            OpenNote_Title.Text = Properties.Settings.Default.TitlesList[Index];
            OpenNote_Desc.Text = Properties.Settings.Default.DescriptionsList[Index];
        }

        public void ApplyButtonStyle(System.Windows.Controls.Button NewButton)
        {
            BrushConverter Converter = new BrushConverter();

            NewButton.Width = 300;
            NewButton.Height = 40;
            NewButton.FontSize = 25;
            NewButton.FontWeight = FontWeights.DemiBold;
            NewButton.Background = (System.Windows.Media.Brush)Converter.ConvertFromString("#363636"); 
            NewButton.Foreground = (System.Windows.Media.Brush)Converter.ConvertFromString("#ffffff");
        }

        private void AppClosing(object sender, CancelEventArgs e)
        {
            if (AddNote.Visibility == Visibility.Collapsed && ImportantInfo.Visibility == Visibility.Collapsed)
                return;

            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Unsaved progress will be lost", "Are you sure you want to quit?", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
                Environment.Exit(0);
            else
                e.Cancel = true;
        }
    }
}