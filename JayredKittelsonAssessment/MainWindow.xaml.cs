using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace JayredKittelsonAssessment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string EmployeeName { get; set; }
        public Reservation ConferenceRoomReservation { get; set; }
        public ObservableCollection<Reservation> ConfRoomResList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ConfRoomResList = new ObservableCollection<Reservation>();
            dgReservations.ItemsSource = ConfRoomResList;
            ConferenceRoomReservation = new Reservation();
            var roomList = new List<string>{
                "Conference Room A",
                "Conference Room B"
            };
            var timeList = new List<DateTime>();
            cbxStartTime.ItemsSource = GetTimeIntervals();
            cbxConferenceRoomName.ItemsSource = roomList;
            DurationTime.ItemsSource = GetDuration();
            EmployeeName = "";
            DataContext = this;
            BtnReserve.IsEnabled = false;
            Clear();
        }

        private void CalButton_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //ConferenceRoomReservation.ReservationDate = calButton.SelectedDate.Value.Date;
        }

        private void BtnReserve_Click(object sender, RoutedEventArgs e)
        {
            ConferenceRoomReservation.ReservationDate = calButton.SelectedDate.Value.Date;
            ConferenceRoomReservation.EmployeeName = txtEmployeeName.Text;
            ConferenceRoomReservation.ConferenceRoomName = cbxConferenceRoomName.SelectedValue.ToString();
            ConferenceRoomReservation.ReservationStartTime = ConferenceRoomReservation.ReservationDate.Date.Add(DateTime.ParseExact(cbxStartTime.SelectedValue.ToString(), "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay);
            ConferenceRoomReservation.Duration = Convert.ToDouble(DurationTime.SelectedValue.ToString());
            ConferenceRoomReservation.ReservationEndTime = ConferenceRoomReservation.ReservationStartTime.AddHours(ConferenceRoomReservation.Duration);
            var isSavable = false;
            if (ConfRoomResList.Count < 1)
            {
                isSavable = true;
            }
            else
            {

                for (var i = 0; i < ConfRoomResList.Count; i++)
                {
                    if (ConfRoomResList[i].ConferenceRoomName == ConferenceRoomReservation.ConferenceRoomName)
                    {
                        if ((ConferenceRoomReservation.ReservationStartTime > ConfRoomResList[i].ReservationStartTime &&
                                ConferenceRoomReservation.ReservationStartTime >= ConfRoomResList[i].ReservationEndTime)
                                ||
                                ConferenceRoomReservation.ReservationStartTime < ConfRoomResList[i].ReservationStartTime)
                        {
                            isSavable = true;
                        }
                        else
                        {
                            isSavable = false;
                            break;
                        }

                    }
                    else
                    {
                        isSavable = true;
                    }
                }
            }

            if (isSavable)
            {
                ConfRoomResList.Add(ConferenceRoomReservation);
                Clear();
            }
            else
            {
                MessageBox.Show("Could not add Reservation.  Please revise selection");
            }
        }

        private void Clear()
        {
            txtEmployeeName.Text = string.Empty;
            cbxConferenceRoomName.SelectedIndex = 0;
            cbxStartTime.SelectedIndex = 0;
            DurationTime.SelectedIndex = 0;
            calButton.SelectedDate = DateTime.Now;
            ConferenceRoomReservation = new Reservation();
        }


        private List<string> GetTimeIntervals()
        {
            return new List<string>{
            "12:00 AM",
            "12:30 AM",
            "1:00 AM",
            "1:30 AM",
            "2:00 AM",
            "2:30 AM",
            "3:00 AM",
            "3:30 AM",
            "4:00 AM",
            "4:30 AM",
            "5:00 AM",
            "5:30 AM",
            "6:00 AM",
            "6:30 AM",
            "7:00 AM",
            "7:30 AM",
            "8:00 AM",
            "8:30 AM",
            "9:00 AM",
            "9:30 AM",
            "10:00 AM",
            "10:30 AM",
            "11:00 AM",
            "11:30 AM",
            "12:00 PM",
            "12:30 PM",
            "1:00 PM",
            "1:30 PM",
            "2:00 PM",
            "2:30 PM",
            "3:00 PM",
            "3:30 PM",
            "4:00 PM",
            "4:30 PM",
            "5:00 PM",
            "5:30 PM",
            "6:00 PM",
            "6:30 PM",
            "7:00 PM",
            "7:30 PM",
            "8:00 PM",
            "8:30 PM",
            "9:00 PM",
            "9:30 PM",
            "10:00 PM",
            "10:30 PM",
            "11:00 PM",
            "11:30 PM"
        };
        }

        private List<string> GetDuration()
        {
            return new List<string>
            {
                "1",
                "1.5",
                "2",
                "2.5",
                "3",
                "3.5",
                "4",
                "4.5",
                "5",
                "5.5",
                "6",
                "6.5",
                "7",
                "7.5",
                "8",
                "8.5",
                "9",
                "9.5",
                "10",
                "10.5",
                "11",
                "11.5",
                "12",
                "12.5",
                "13",
                "13.5",
                "14",
                "14.5",
                "15",
                "15.5",
                "16",
                "16.5",
                "17",
                "17.5",
                "18",
                "18.5",
                "19",
                "19.5",
                "20",
                "20.5",
                "21",
                "21.5",
                "22",
                "22.5",
                "23",
                "23.5",
                "24",

            };
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ConfRoomResList.RemoveAt(dgReservations.SelectedIndex);
        }

        private void txtEmployeeName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (txtEmployeeName.Text.Length > 0)
            {
                BtnReserve.IsEnabled = true;
            }
            else
            {
                BtnReserve.IsEnabled = false;
            }
        }
    }
}
