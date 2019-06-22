using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;


namespace CustomerInformation.Resources.Model
{
    class Init
    {
        static public SQLiteConnection db { get; set; }
        public Init()
        {
            string dbPath = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                "database.db3"
                );
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Customer>();
        }
    }
}