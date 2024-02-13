using Dapper;
using OneToMany3_22_10Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace OneToMany3_22_10Dapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
            using (var connection = new SqlConnection(conn))
            {
                var sql = @"
                        SELECT S.StudentId,S.Firstname,S.Age,S.GroupId,
                        G.GroupId,G.Title
                        FROM Students AS S
                        INNER JOIN Groups AS G
                        ON S.GroupId=G.GroupId
            ";

                var students = connection.Query<Student, Entities.Group, Student>(sql,
                    (s, g) =>
                    {
                        s.Group = g;
                        return s;
                    }, splitOn: nameof(Student.GroupId));

                studentsGrid.ItemsSource = students;
            }


            using (var connection=new SqlConnection(conn))
            {

                var sql = @"SELECT G.GroupId,G.Title,S.StudentId,S.Firstname,
                            S.Age
                            FROM Groups AS G
                            INNER JOIN Students AS S
                            ON G.GroupId=S.GroupId";

                var groups = connection.Query<Entities.Group, Student, Entities.Group>(sql,
                    (group, student) =>
                    {
                        group.Students.Add(student);
                        student.GroupId = group.GroupId;
                        student.Group = group;
                        return group;
                    }, splitOn: nameof(Student.GroupId)).ToList();
                groupsGrid.ItemsSource = groups;
            }


        }
    }
}
