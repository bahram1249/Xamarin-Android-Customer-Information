using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CustomerInformation.Resources.Model;
using SQLite;

namespace CustomerInformation.Activity
{
    [Activity(Label = "AddCustomerActivity")]
    public class AddCustomerActivity : AppCompatActivity
    {
        private readonly string _dbPath = Path.Combine(
                 System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                 "database.db3"
             );

        private SQLiteConnection _db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.activity_add_customer);

            // set db path
            _db = new SQLiteConnection(_dbPath);

            // add a customer on customer list
            var addButton = FindViewById<Button>(Resource.Id.Add_Customer_Activity_Button_Add);
            addButton.Click += AddButtonOnClick;
        }

        private void AddButtonOnClick(object sender, EventArgs e)
        {
            // get details of customer from view
            var name = FindViewById<EditText>(Resource.Id.Add_Customer_Activity_EditBox_Name);
            var address = FindViewById<EditText>(Resource.Id.Add_Customer_Activity_EditBox_Address);
            var phone = FindViewById<EditText>(Resource.Id.Add_Customer_Activity_EditBox_Phone);

            // create a customer based on details of field on editBox
            var customer = new Customer()
            {
                Name = name.Text,
                Address = address.Text,
                PhoneNumber = phone.Text
            };

            // insert customer into database and close this activity
            _db.Insert(customer);
            this.Finish();
        }
    }
}