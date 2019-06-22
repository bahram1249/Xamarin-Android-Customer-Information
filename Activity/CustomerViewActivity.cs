
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using SQLite;
using System;
using System.IO;
using CustomerInformation.Resources.Model;


namespace CustomerInformation.Activity
{
    [Activity(Label = "CustomerViewActivity")]
    public class CustomerViewActivity : AppCompatActivity
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
            SetContentView(Resource.Layout.activity_customer_view);

            // set db path
            _db = new SQLiteConnection(_dbPath);

            GetInformationToTextView();


            var deleteCustomer = FindViewById<Button>(Resource.Id.Customer_View_Activity_Button_Delete);
            deleteCustomer.Click += DeleteCustomerOnClick;
        }

        private void DeleteCustomerOnClick(object sender, EventArgs e)
        {
            // get id value passed from another activity
            var id = Convert.ToInt32(Intent.GetStringExtra("Id"));

            // delete a customer with this given id
            _db.Delete<Customer>(id);

            // close this activity
            this.Finish();
        }

        private void GetInformationToTextView()
        {
            var (txtViewId, txtViewName, txtViewAddress, txtViewPhone) = GetCustomerTextViewsFromView();
            
            // Get customer id passed from another activity
            var customerId = Convert.ToInt32(Intent.GetStringExtra(name:"Id"));

            // Find the customer with this given id
            var customer = _db.Table<Customer>().FirstOrDefault(item => item.Id == customerId);

            // Change textView of customer details
            txtViewId.Text = "آی دی : " + customer.Id.ToString();
            txtViewName.Text = "نام مشتری : " + customer.Name;
            txtViewAddress.Text = "آدرس مشتری : " + customer.Address;
            txtViewPhone.Text = "شماره تلفن مشتری : " + customer.PhoneNumber;
        }

        private (TextView Id, TextView Name, TextView Address, TextView Phone) GetCustomerTextViewsFromView()
        {
            var id = FindViewById<TextView>(Resource.Id.Customer_View_Activity_Text_View_Id);
            var name = FindViewById<TextView>(Resource.Id.Customer_View_Activity_Text_View_Name);
            var address = FindViewById<TextView>(Resource.Id.Customer_View_Activity_Text_View_Address);
            var phone = FindViewById<TextView>(Resource.Id.Customer_View_Activity_Text_View_Phone);

            return (id, name, address, phone);
        }
    }
}