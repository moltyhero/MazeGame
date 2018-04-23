﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Network namespaces
using System.Net;
using NetworkCommsDotNet;
using NetworkCommsDotNet.DPSBase;
using NetworkCommsDotNet.Tools;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using System.Net.Sockets;

namespace TheMaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;
        public static OnlineOptionsWindow onlineOptionsWindow;
        public static MazeNode playerCurrentLocation; // The current location of the user
        public static string myIP = "My IP is ";

        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;
            ScreenOrginizer screenOrginizer = new ScreenOrginizer(mazeWindow.Width, mazeWindow.Height, Int32.Parse(mazeRows.Text), Int32.Parse(mazeCols.Text));
            screenOrginizer.CreateMaze(mainStackPanel);
            GetLocalIPAddress();
            ipTextBox.Text = myIP;

            //NetworkComms.SendObject("MazeStackPanel", "127.0.0.1", 10000, mainStackPanel);
        }

        // Gets my Local IP and put it in MyIP
        public static void GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myIP = myIP + ip.ToString();
                }
            }
            //throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        // To make sure the user enters numbers only in the maze size textboxes
        private void MazeSizeTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void GenerateMaze()
        {
            mainStackPanel.Children.Clear();
            InitializeComponent();
            ScreenOrginizer screenOrginizer = new ScreenOrginizer(mazeWindow.Width, mazeWindow.Height, Int32.Parse(mazeRows.Text), Int32.Parse(mazeCols.Text));
            screenOrginizer.CreateMaze(mainStackPanel);
            generateMazeButton.IsEnabled = true;
        }

        // Maze regenerate
        private void Generate_Maze_Click(object sender, RoutedEventArgs e)
        {
            GenerateMaze();
        }

        // Win condition handle
        private void EndPointArrival ()
        {
            if (playerCurrentLocation.Equals(ScreenOrginizer.last))
            {
                generateMazeButton.IsEnabled = false;
                winPopup.IsOpen = true;
            }
        }


        /// <summary>
        /// Main method for movement. Create a movement illusion for the player. Also verefying wheter the movement is valid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                if (CheckWhetherValidMovement(playerCurrentLocation.Neighbors[MazeNode.West]))
                {
                    playerCurrentLocation.Bounds.Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                    playerCurrentLocation = playerCurrentLocation.Neighbors[MazeNode.West];
                    playerCurrentLocation.Bounds.Fill = new SolidColorBrush(System.Windows.Media.Colors.Orange);
                }
            }
            else if (e.Key == Key.Right)
            {
                if (CheckWhetherValidMovement(playerCurrentLocation.Neighbors[MazeNode.East]))
                {
                    playerCurrentLocation.Bounds.Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                    playerCurrentLocation = playerCurrentLocation.Neighbors[MazeNode.East];
                    playerCurrentLocation.Bounds.Fill = new SolidColorBrush(System.Windows.Media.Colors.Orange);
                }
            }
            else  if (e.Key == Key.Up)
            {
                if (CheckWhetherValidMovement(playerCurrentLocation.Neighbors[MazeNode.North]))
                {
                    playerCurrentLocation.Bounds.Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                    playerCurrentLocation = playerCurrentLocation.Neighbors[MazeNode.North];
                    playerCurrentLocation.Bounds.Fill = new SolidColorBrush(System.Windows.Media.Colors.Orange);
                }
            }
            else if (e.Key == Key.Down)
            {
                if (CheckWhetherValidMovement(playerCurrentLocation.Neighbors[MazeNode.South]))
                {
                    playerCurrentLocation.Bounds.Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                    playerCurrentLocation = playerCurrentLocation.Neighbors[MazeNode.South];
                    playerCurrentLocation.Bounds.Fill = new SolidColorBrush(System.Windows.Media.Colors.Orange);
                }
            }
            EndPointArrival(); // Handle the win condition.
        }

        private bool CheckWhetherValidMovement (MazeNode goingTo)
        {
            if (goingTo == null)
            {
                return false;
            }
            else if (goingTo.Predecessor == playerCurrentLocation || goingTo == playerCurrentLocation.Predecessor)
            {
                return true;
            }
            return false;
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            winPopup.IsOpen = false;
            GenerateMaze();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Online_Click(object sender, RoutedEventArgs e)
        {
            onlineOptionsWindow = new OnlineOptionsWindow();
            onlineOptionsWindow.Show();
        }

        public void ShowMyIP()
        {
            ipTextBox.Visibility = Visibility.Visible;
        }
    }
    
    
}
