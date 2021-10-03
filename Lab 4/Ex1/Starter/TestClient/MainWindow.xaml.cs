using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initiate the reactor shutdown using a Switch object
        /// Record details of shutdown status in a TextBlock - recording all exceptions thrown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            // TODO - Add exception handling
            this.textBlock1.Text = "Initiating test sequence: " + DateTime.Now.ToLongTimeString();
            SwitchDevices.Switch sd = new SwitchDevices.Switch();

            // Step 1 - disconnect from the Power Generator
            if (sd.DisconnectPowerGenerator() == SwitchDevices.SuccessFailureResult.Fail)
            {
                this.textBlock1.Text += "\nStep 1: Failed to disconnect power generation system";
            }
            else
            {
                this.textBlock1.Text += "\nStep 1: Successfully disconnected power generation system";
            }


            // Step 2 - Verify the status of the Primary Coolant System
            switch (sd.VerifyPrimaryCoolantSystem())
            {
                case SwitchDevices.CoolantSystemStatus.OK:
                    this.textBlock1.Text += "\nStep 2: Primary coolant system OK";
                    break;
                case SwitchDevices.CoolantSystemStatus.Check:
                    this.textBlock1.Text += "\nStep 2: Primary coolant system requires manual check";
                    break;
                case SwitchDevices.CoolantSystemStatus.Fail:
                    this.textBlock1.Text += "\nStep 2: Problem reported with primary coolant system";
                    break;
            }
            

            // Step 3 - Verify the status of the Backup Coolant System
            switch (sd.VerifyBackupCoolantSystem())
            {
                case SwitchDevices.CoolantSystemStatus.OK:
                    this.textBlock1.Text += "\nStep 3: Backup coolant system OK";
                    break;
                case SwitchDevices.CoolantSystemStatus.Check:
                    this.textBlock1.Text += "\nStep 3: Backup coolant system requires manual check";
                    break;
                case SwitchDevices.CoolantSystemStatus.Fail:
                    this.textBlock1.Text += "\nStep 3: Backup reported with primary coolant system";
                    break;
            }


            // Step 4 - Record the core temperature prior to shutting down the reactor
            this.textBlock1.Text += "\nStep 4: Core temperature before shutdown: " + sd.GetCoreTemperature();


            // Step 5 - Insert the control rods into the reactor
            if (sd.InsertRodCluster() == SwitchDevices.SuccessFailureResult.Success)
            {
                this.textBlock1.Text += "\nStep 5: Control rods successfully inserted";
            }
            else
            {
                this.textBlock1.Text += "\nStep 5: Control rod insertion failed";
            }

            // Step 6 - Record the core temperature after shutting down the reactor
            this.textBlock1.Text += "\nStep 6: Core temperature after shutdown: " + sd.GetCoreTemperature();


            // Step 7 - Record the core radiation levels after shutting down the reactor
            this.textBlock1.Text += "\nStep 7: Core radiation level after shutdown: " + sd.GetRadiationLevel();


            // Step 8 - Broadcast "Shutdown Complete" message
            sd.SignalShutdownComplete();
            this.textBlock1.Text += "\nStep 8: Broadcasting shutdown complete message";


            this.textBlock1.Text += "\nTest sequence complete: " + DateTime.Now.ToLongTimeString();
        }
    }
}
