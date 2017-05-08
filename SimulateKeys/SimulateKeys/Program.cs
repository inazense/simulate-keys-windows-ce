using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using Terranova.API;

namespace SimulateKeys
{
    class Program
    {
        // Properties
        const int KEYEVENT_KEYUP = 0x2; // Press code
        const int KEYEVENT_KEYDOWN = 0x0; // Release code

        // Methods

        /// <summary>
        /// Set the process you want to send the key as foreground window
        /// </summary>
        [DllImport("coredll.dll", EntryPoint = "SetForegroundWindow")]
        static extern int SetForegroundWindow(IntPtr point);

        /// <summary>
        /// Send the key using .net compact framework 
        /// </summary>
        [DllImport("coredll.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// Get the PID of the process passed by name
        /// </summary>
        /// <param name="processName">Process name</param>
        /// <returns>PID of the process with the same name as the parameter</returns>
        private static IntPtr getMyProcessPID(string processName)
        {
            IntPtr pid = new IntPtr();
            // List all the running processes in the device
            ProcessInfo[] processesList = ProcessCE.GetProcesses();

            // Loop all the processes searching the name of the proccess we want to send keys
            foreach (ProcessInfo process in processesList)
            {
                if (process.FullPath.Equals(processName))
                {
                    pid = process.Pid;
                    break;
                }
            }

            return pid;
        }

        // Main
        static void Main(string[] args)
        {
            // What key I will to send in hexadecimal. All the possible key are in Keys.cs
            byte key = Keys.ENTER;

            // PID of the process to which I will send the key
            IntPtr myProcessPID = getMyProcessPID("MyProcess.exe");
            
            if (myProcessPID != null)
            {
                // Get the process with the same PID as myProcessPID
                Process myProcess = Process.GetProcessById(myProcessPID.ToInt32());

                if (myProcess != null)
                {
                    // Set myProcess as foreground
                    IntPtr foregroundProcess = myProcess.MainWindowHandle;
                    SetForegroundWindow(foregroundProcess);

                    // Send the key
                    keybd_event(key, 0, KEYEVENT_KEYDOWN, 0); // Press the key
                    keybd_event(key, 0, KEYEVENT_KEYUP, 0); // Release the key
                }
            }
        }
    }
}
