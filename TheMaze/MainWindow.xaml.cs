﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TheMaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateMaze();
        }

        // Calculate the desired size of the rectangle
        private double calcRectSize(double StackHeight, double StackWidth, int rows, int cols)
        {
            double HeightSize;
            double WidthSize;
            HeightSize = StackHeight / rows;
            WidthSize = StackWidth / cols;
            return Math.Min(HeightSize, WidthSize);
        }

        private void CreateMaze()
        {
            int rows = 20;
            int cols = 22;
            // helper.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            double w = mazeWindow.Width*4/6;
            double h = mazeWindow.Height*4/6;
            // double size = calcRectSize(mainStackPanel.ActualHeight, mainStackPanel.ActualWidth, rows, cols);
            double size = calcRectSize(h, w, rows, cols);

            // Get the maze size
            /*System.Console.WriteLine("Enter row");
            rows = Int32.Parse(Console.ReadLine());
            System.Console.WriteLine("Enter col");
            cols = Int32.Parse(Console.ReadLine());*/



            // Add stack pannels to the stack pannels
            for (int i = 0; i<cols; i++)
            {
                StackPanel stackPannel = new StackPanel();
                stackPannel.Name = ("col" +i);
                

                for (int j = 0; j < rows; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Height = size;
                    rect.Width = size;
                    rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.Gray);
                    rect.StrokeThickness = 1;
                    rect.Stroke = Brushes.Violet;
                    stackPannel.Children.Add(rect);
                }

                mainStackPanel.Children.Add(stackPannel);
            }

        }
    }
    
    
}
