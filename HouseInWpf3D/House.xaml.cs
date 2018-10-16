using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace HouseInWpf3D
{
    /// <summary>
    /// Interaction logic for House.xaml
    /// </summary>
    public partial class House : Window
    {
        public House()
        {
            InitializeComponent();

            DrawBalconyFences();

            DrawWindows();

            DrawDoors();
        }

        /// <summary>
        /// Draw balcony fence.
        /// </summary>
        private void DrawBalconyFences()
        {
            DiffuseMaterial blackBrush = new DiffuseMaterial(new SolidColorBrush(Colors.Black));

            // East side fence
            DrawFence(blackBrush, 21, 0.15, 3.5, new Point3D(-35.5, 25.0, 12.0), new Point3D(-35.5, 14.0, 12.0));

            // South side fence 1
            DrawFence(blackBrush, 23, 0.15, 3.5, new Point3D(-34.1, 26.5, 12.0), new Point3D(-22.1, 26.5, 12.0));

            // South side fence 2
            DrawFence(blackBrush, 37, 0.15, 3.5, new Point3D(-20.1, 26.5, 12.0), new Point3D(-1.1, 26.5, 12.0));

            // South side fence 3
            DrawFence(blackBrush, 27, 0.15, 3.5, new Point3D(-0.1, 26.5, 12.0), new Point3D(14, 26.5, 12.0));
        }

        /// <summary>
        /// Draw a balcony fence
        /// </summary>
        /// <param name="material">Brush for the fence.</param>
        /// <param name="barcount">Number of verticcal bars for each fence.</param>
        /// <param name="barsize">Size of the verticcal bars.</param>
        /// <param name="barheight">Height of the verticcal bars.</param>
        /// <param name="p1">Starting point of the top horizontal bar.</param>
        /// <param name="p2">End  point of the top horizontal bar.</param>
        private void DrawFence(Material material, int barcount, double barsize, double barheight, Point3D p1, Point3D p2)
        {
            MeshGeometry3D post = this.viewport.FindResource("post") as MeshGeometry3D;

            ScaleTransform3D scaleV = new ScaleTransform3D(barsize, barsize, barheight);

            double length = Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y) + (p2.Z - p1.Z) * (p2.Z - p1.Z));
            double n1 = 1.0 / ((double)(barcount + 1));
            double x0 = p1.X;
            double y0 = p1.Y;
            double z0 = p1.Z;
            double dx = (p2.X - p1.X) * n1;
            double dy = (p2.Y - p1.Y) * n1;
            double dz = (p2.Z - p1.Z) * n1;
            double rotateAngle = 90.0;
            Vector3D rotateAxis = (Math.Abs(p2.X - p1.X) < 0.00001) ? new Vector3D(1.0, 0.0, 0.0) : new Vector3D(0.0, 1.0, 0.0);

            // Top horizontal bar
            GeometryModel3D bar = new GeometryModel3D(post, material);
            Transform3DGroup transforms = new Transform3DGroup();
            TranslateTransform3D shift = new TranslateTransform3D(x0, y0, z0 + barheight);
            ScaleTransform3D scaleH = new ScaleTransform3D(barsize, barsize, length);
            RotateTransform3D rotate = new RotateTransform3D(new AxisAngleRotation3D(rotateAxis, rotateAngle));
            transforms.Children.Add(scaleH);
            transforms.Children.Add(rotate);
            transforms.Children.Add(shift);
            bar.Transform = transforms;
            exteriorWalls.Children.Add(bar);

            for (int i = 0; i < barcount; i++)
            {
                x0 += dx;
                y0 += dy;
                z0 += dz;
                bar = new GeometryModel3D(post, material);
                transforms = new Transform3DGroup();
                shift = new TranslateTransform3D(x0, y0, z0);
                transforms.Children.Add(scaleV);
                transforms.Children.Add(shift);
                bar.Transform = transforms;
                exteriorWalls.Children.Add(bar);
            }
        }

        /// <summary>
        /// Draw all windows
        /// </summary>
        private void DrawDoors()
        {
            DiffuseMaterial materialBorder = new DiffuseMaterial(new SolidColorBrush(Colors.BlanchedAlmond));
            DiffuseMaterial materialDoor = new DiffuseMaterial(new SolidColorBrush(Colors.White));
            DiffuseMaterial materialFrontDoor = new DiffuseMaterial(new SolidColorBrush(Colors.SaddleBrown));
            double h1 = 1.5;
            double h3 = 3.0;
            double h5 = 5.0;
            double h6 = 6.0;
            double h7 = 6.7;
            double h8 = 8.0;
            double w2 = 2.0;
            double w3 = 3.0;
            double w4 = 4.0;
            double w5 = 5.0;
            double w6 = 6.0;
            double w8 = 8.0;
            double w16 = 16.0;
            double width = 0.35;
            double x1, y1, z1;

            #region Front
            // Doors in the front side
            x1 = -14.0;
            y1 = -31.01;
            z1 = 0.0;
            DrawDoor(materialFrontDoor, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w6, y1, z1 + h8), width);
            #endregion

            #region West
            // Doors in the right hand side
            x1 = 36.01;
            y1 = -25.0;
            z1 = 0.0;
            DrawDoor(materialDoor, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 + w3, z1 + h7), width);
            y1 += w3 + 1.5;
            z1 = -0.4;
            DrawDoor(materialDoor, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 + w8, z1 + h7), width);
            y1 += w8 + 1.5;
            DrawDoor(materialDoor, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 + w16, z1 + h7), width);
            y1 += w16 + 5.0;
            z1 = 3.0;
            #endregion

            #region Back
            // Doors in the back
            x1 = 3.0;
            y1 = 14.01;
            z1 = 0.0;
            DrawDoor(materialDoor, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h7), width);
            x1 = 10.0;
            y1 = 14.01;
            z1 = 11.0;
            DrawDoor(materialDoor, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w6, y1, z1 + h7), width);
            x1 = -21.0;
            y1 = 14.01;
            z1 = 11.0;
            DrawDoor(materialDoor, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h7), width);
            #endregion

            #region East
            // Doors in the left hand side
            x1 = 13.99;
            y1 = 25.0;
            z1 = 0.0;
            DrawDoor(materialDoor, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 - w6, z1 + h7), width);
            #endregion
        }

        /// <summary>
        /// Draw balcony fence.
        /// </summary>
        private void DrawWindows()
        {
            DiffuseMaterial materialWindow = new DiffuseMaterial(new SolidColorBrush(Colors.LightGray));
            DiffuseMaterial materialBorder = new DiffuseMaterial(new SolidColorBrush(Colors.White));
            double h1 = 1.5;
            double h3 = 3.0;
            double h5 = 5.0;
            double h6 = 6.0;
            double h7 = 6.7;
            double w2 = 2.0;
            double w3 = 3.0;
            double w4 = 4.0;
            double w5 = 5.0;
            double w8 = 8.0;
            double w16 = 16.0;
            double width = 0.35;
            double x1, y1, z1;

            #region Front
            // Windows in the front side
            // Bedroom #3
            x1 = -32.0;
            y1 = -31.01;
            z1 = 13.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w4, y1, z1 + h5), width);
            x1 += 8;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w4, y1, z1 + h5), width);

            // Porch
            x1 = -15.0;
            y1 = -38.01;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w2, y1, z1 + h3), width, true);
            x1 += w2 + 1.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w2, y1, z1 + h3), width, true);
            x1 += w2 + 1.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w2, y1, z1 + h3), width, true);

            // Bedroom #4
            x1 = -2.0;
            y1 = -31.01;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h5), width);
            x1 += 5.5;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h5), width);
            x1 += 5.5;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h5), width);

            // Bedroom #5
            x1 = 18.5;
            y1 = -28.01;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h5), width);
            x1 = 27.5;
            y1 = -30.01;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h5), width);

            // Study room
            x1 = -32.0;
            y1 = -31.01;
            z1 = 3.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w4, y1, z1 + h5), width);
            x1 = -26.0;
            z1 = 2.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h6), width);
            x1 += w3 + 1.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h6), width);

            // Dining room 
            x1 = -2.0;
            y1 = -31.01;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h5), width, true);
            x1 += 5.5;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h5), width, true);
            x1 += 5.5;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h5), width, true);

            // Garage
            x1 = 27.5;
            y1 = -32.01;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w3, y1, z1 + h5), width);
            //x1 = 19;
            //y1 = -30.01;
            //z1 = 3.5;
            //DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 + w2, y1, z1 + w2), width);

            #endregion

            #region West
            // Windows in the right hand side
            x1 = 36.01;
            y1 = 10.0;
            z1 = 3.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 + w3, z1 + h5), width);
            y1 += 5.0;
            z1 = 2.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 + w4, z1 + h6), width);
            y1 += 5.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 + w4, z1 + h6), width);
            y1 += 5.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 + w4, z1 + h6), width);
            x1 = 34.01;
            y1 = -18.0;
            z1 = 15.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 + w2, z1 + h3), width);
            y1 = -6.0;
            z1 = 13.5;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 + w3, z1 + h5), width);
            y1 += w3 + 0.5;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 + w3, z1 + h5), width);
            #endregion

            #region Back
            // Windows in the rear side
            // Exercise room
            x1 = 34.5;
            y1 = 30.01;
            z1 = 2.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w4, y1, z1 + h7), width);
            x1 -= w5;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w4, y1, z1 + h7), width);
            x1 -= w5;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w4, y1, z1 + h7), width);
            x1 -= w5;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w4, y1, z1 + h7), width);
            x1 = 26.0;
            z1 = 13.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w4, y1, z1 + h5), width);

            // Breakfast room
            x1 = 12.0;
            y1 = 14.01;
            z1 = 2.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h6), width);
            x1 -= w4;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h6), width);

            // Family room
            x1 = -3.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h6), width);
            x1 -= w4;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h6), width);
            x1 -= w4;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h6), width);
            x1 -= w4;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h6), width);

            // Master bedroom
            x1 = -22.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h6), width);
            x1 -= w4;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h6), width);
            x1 -= w4;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h6), width);

            // Family room (upstair)
            x1 = -3.0;
            z1 = 13.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h5), width);
            x1 -= w4;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h5), width);
            x1 -= w4;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h5), width);
            x1 -= w4;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h5), width);

            // Master bedroom
            x1 = -26.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h5), width);
            x1 -= w4;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1 - w3, y1, z1 + h5), width);
            #endregion

            #region East
            // Windows in the left hand side
            // Exercise room door area
            x1 = 13.99;
            y1 = 29.0;
            z1 = 2.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 - w3, z1 + h5), width);
            y1 = 18.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 - w3, z1 + h5), width);

            // East side wall
            x1 = -36.01;
            y1 = -6.0;
            z1 = 17.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 - w5, z1 + h1), width);
            y1 = -13.0;
            z1 = 3.0;
            DrawWindow(materialWindow, materialBorder, new Point3D(x1, y1, z1), new Point3D(x1, y1 - w5, z1 + h5), width);
            #endregion
        }

        /// <summary>
        /// Draw a window
        /// </summary>
        /// <param name="matWindow">Brush for the window.</param>
        /// <param name="matBorder">Brush for the window borders.</param>
        /// <param name="p1">Bottom left corner point of the window.</param>
        /// <param name="p2">Top right corner point of the window.</param>
        /// <param name="borderWidth">Border width</param>
        /// <param name="hasArch">Top of the window is an arch.</param>
        private void DrawWindow(Material matWindow, Material matBorder, Point3D p1, Point3D p2, double width, bool hasArch = false)
        {
            // Draw window itself
            DrawRectangle(matWindow, p1, p2);

            // Calculate the coordinates of the left and right borders.
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double dxL = 0.0;
            double dxR = 0.0;
            double dyL = 0.0;
            double dyR = 0.0;
            double topZ = p2.Z + width;
            double bottomZ = p1.Z - width;

            if (dx > 1.0e-6)
            {
                dxL = -width;
                dxR = width;
            }
            else if (dx < -1.0e-6)
            {
                dxL = width;
                dxR = -width;
            }

            if (dy > 1.0e-6)
            {
                dyL = -width;
                dyR = width;
            }
            else if (dy < -1.0e-6)
            {
                dyL = width;
                dyR = -width;
            }

            // Draw top border
            if (hasArch)
                DrawArch(matWindow, matBorder, p1, p2, width);
            else
                DrawRectangle(matBorder, new Point3D(p1.X, p1.Y, p2.Z), new Point3D(p2.X, p2.Y, topZ));

            // Draw bottom border
            DrawRectangle(matBorder, new Point3D(p1.X, p1.Y, bottomZ), new Point3D(p2.X, p2.Y, p1.Z));

            // Draw left border
            DrawRectangle(matBorder, new Point3D(p1.X + dxL, p1.Y + dyL, bottomZ), new Point3D(p1.X, p1.Y, topZ));

            // Draw right border
            DrawRectangle(matBorder, new Point3D(p2.X, p2.Y, bottomZ), new Point3D(p2.X + dxR, p2.Y + dyR, topZ));
        }

        /// <summary>
        /// Draw a door
        /// </summary>
        /// <param name="matDoor">Brush for the door.</param>
        /// <param name="matBorder">Brush for the door borders.</param>
        /// <param name="p1">Bottom left corner point of the door.</param>
        /// <param name="p2">Top right corner point of the door.</param>
        /// <param name="borderWidth">Border width</param>
        private void DrawDoor(Material matDoor, Material matBorder, Point3D p1, Point3D p2, double width)
        {
            // Draw window itself
            DrawRectangle(matDoor, p1, p2);

            // Calculate the coordinates of the left and right borders.
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double dxL = 0.0;
            double dxR = 0.0;
            double dyL = 0.0;
            double dyR = 0.0;
            double topZ = p2.Z + width;
            double bottomZ = p1.Z;

            if (dx > 1.0e-6)
            {
                dxL = -width;
                dxR = width;
            }
            else if (dx < -1.0e-6)
            {
                dxL = width;
                dxR = -width;
            }

            if (dy > 1.0e-6)
            {
                dyL = -width;
                dyR = width;
            }
            else if (dy < -1.0e-6)
            {
                dyL = width;
                dyR = -width;
            }

            // Draw top border
            DrawRectangle(matBorder, new Point3D(p1.X, p1.Y, p2.Z), new Point3D(p2.X, p2.Y, topZ));

            // Draw left border
            DrawRectangle(matBorder, new Point3D(p1.X + dxL, p1.Y + dyL, bottomZ), new Point3D(p1.X, p1.Y, topZ));

            // Draw right border
            DrawRectangle(matBorder, new Point3D(p2.X, p2.Y, bottomZ), new Point3D(p2.X + dxR, p2.Y + dyR, topZ));
        }

        /// <summary>
        /// Draw a window
        /// </summary>
        /// <param name="material">Brush for the window.</param>
        /// <param name="p1">Bottom left corner point of the window.</param>
        /// <param name="p2">Top right corner point of the window.</param>
        private void DrawRectangle(Material material, Point3D p1, Point3D p2)
        {
            // The geometry specifes the shape of the 3D plane. 
            MeshGeometry3D winGeometry = new MeshGeometry3D();

            // Create a collection of vertex positions for the MeshGeometry3D. 
            Point3DCollection winPositions = new Point3DCollection();
            winPositions.Add(new Point3D(p1.X, p1.Y, p2.Z));
            winPositions.Add(p1);
            winPositions.Add(p2);
            winPositions.Add(new Point3D(p2.X, p2.Y, p1.Z));
            winGeometry.Positions = winPositions;

            // Create a collection of triangle indices for the MeshGeometry3D.
            Int32Collection winTriangleIndices = new Int32Collection();
            winTriangleIndices.Add(0);
            winTriangleIndices.Add(1);
            winTriangleIndices.Add(2);
            winTriangleIndices.Add(1);
            winTriangleIndices.Add(3);
            winTriangleIndices.Add(2);
            winGeometry.TriangleIndices = winTriangleIndices;

            // Draw the window
            GeometryModel3D window = new GeometryModel3D(winGeometry, material);
            exteriorWalls.Children.Add(window);
        }

        /// <summary>
        /// Draw an arch
        /// </summary>
        /// <param name="matWindow">Brush for the arch.</param>
        /// <param name="matBorder">Brush for the arch borders.</param>
        /// <param name="p1">Bottom left corner point of the arch.</param>
        /// <param name="p2">Bottom right corner point of the arch.</param>
        /// <param name="borderWidth">Border width</param>
        private void DrawArch(Material matWindow, Material matBorder, Point3D p1, Point3D p2, double width, bool isFull=false)
        {

            // The geometry specifes the shape of the 3D plane. 
            MeshGeometry3D winGeometryIn = new MeshGeometry3D();
            MeshGeometry3D winGeometryOut = new MeshGeometry3D();

            // Create a collection of vertex positions for the MeshGeometry3D. 
            Point3DCollection winPositionsIn = new Point3DCollection();
            Point3DCollection winPositionsOut = new Point3DCollection();

            // Create a collection of triangle indices for the MeshGeometry3D.
            Int32Collection winTriangleIndicesIn = new Int32Collection();
            Int32Collection winTriangleIndicesOut = new Int32Collection();

            // Calculate the coordinates of the left and right borders.
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double dz = 0.0;
            double radiusIn = 0.5 * Math.Sqrt(dx * dx + dy * dy + dz * dz);
            double radiusOut = radiusIn + width;
            double ratio = radiusOut / radiusIn;
            int num = 18;
            double dAngle = Math.Atan(1.0) * 4.0 / ((double)num);
            double angle = 0.0;

            if (isFull)
                num += num;

            // Arch center
            double x0 = (p1.X + p2.X) * 0.5;
            double y0 = (p1.Y + p2.Y) * 0.5;
            double z0 = p2.Z;
            dz = radiusIn + radiusIn;
            winPositionsIn.Add(new Point3D(x0, y0, z0));

            for (int i = 0; i <= num; i++)
            {
                // sin(angle) and cos(angle)
                double ratioZ = Math.Sin(angle);
                double ratioXY = Math.Cos(angle);

                // Coordinate of the inner perimeter
                double xIn = x0 + 0.5 * dx * ratioXY;
                double yIn = y0 + 0.5 * dy * ratioXY;
                double zIn = z0 + 0.5 * dz * ratioZ;

                // Coordinate of the outer perimeter
                double xOut = x0 + (xIn - x0) * ratio;
                double yOut = y0 + (yIn - y0) * ratio;
                double zOut = z0 + (zIn - z0) * ratio;

                Point3D pIn = new Point3D(xIn, yIn, zIn);
                Point3D pOut = new Point3D(xOut, yOut, zOut);

                winPositionsIn.Add(pIn);
                winPositionsOut.Add(pIn);
                winPositionsOut.Add(pOut);

                if (i > 0)
                {
                    // Inner triangle
                    winTriangleIndicesIn.Add(0);
                    winTriangleIndicesIn.Add(i);
                    winTriangleIndicesIn.Add(i + 1);

                    // Two outer border triangles
                    int index1 = i + i - 2;
                    int index2 = i + i - 1;
                    int index3 = i + i;
                    int index4 = i + i + 1;

                    winTriangleIndicesOut.Add(index1);
                    winTriangleIndicesOut.Add(index2);
                    winTriangleIndicesOut.Add(index4);

                    winTriangleIndicesOut.Add(index1);
                    winTriangleIndicesOut.Add(index4);
                    winTriangleIndicesOut.Add(index3);
                }

                angle += dAngle;
            }

            // Draw the window (inner)
            winGeometryIn.Positions = winPositionsIn;
            winGeometryIn.TriangleIndices = winTriangleIndicesIn;
            GeometryModel3D windowIn = new GeometryModel3D(winGeometryIn, matWindow);
            exteriorWalls.Children.Add(windowIn);

            // Draw the border (outer
            winGeometryOut.Positions = winPositionsOut;
            winGeometryOut.TriangleIndices = winTriangleIndicesOut;
            GeometryModel3D windowOut = new GeometryModel3D(winGeometryOut, matBorder);
            exteriorWalls.Children.Add(windowOut);
        }
    }
}
