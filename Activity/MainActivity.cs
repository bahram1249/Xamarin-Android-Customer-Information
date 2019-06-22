using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using System.Collections.Generic;
using CustomerInformation.Resources.Model;
using System.IO;
using SQLite;
using System.Linq;
using Android.Content;
using Android.Support.Design.Widget;

namespace CustomerInformation.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private readonly string _dbPath = Path.Combine(
                         System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                         "database.db3"
                     );
        private SQLiteConnection _db;
        private ListView _listView;
        private TableQuery<Customer> _customers;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // connect to sql lite and initialized create table !
            _db = new SQLiteConnection(_dbPath);
            _db.CreateTable<Customer>();


            // find fab button for adding a customer from view
            var fabButton = FindViewById<FloatingActionButton>(Resource.Id.main_activity_fab);
            fabButton.Click += Fab_Button_Click;

            /**
              when click in an item of customer listView
                go to new activity for showing the details of customer
             **/
            _listView = FindViewById<ListView>(Resource.Id.main_activity_list_view);
            _listView.ItemClick += ListView_ItemClick;

            GetDataIntoListView();
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var id = _customers.ToList()[e.Position].Id.ToString();
            var intent = new Intent(this, typeof(CustomerViewActivity));
            intent.PutExtra("Id", id);
            StartActivity(intent);
        }

        protected override void OnResume()
        {
            GetDataIntoListView();
            base.OnResume();
        }
        private void Fab_Button_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(AddCustomerActivity));
        }
        private void GetDataIntoListView()
        {
            _customers = _db.Table<Customer>();
            var customersList = _customers.Select(item=>item.Name).ToList();
            var arrayAdapter = new ArrayAdapter<string>(this,
                                                                        Android.Resource.Layout.SimpleListItem1,
                                                                        customersList);

            _listView.Adapter = arrayAdapter;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}