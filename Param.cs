using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPOS_Online
{
    public class Param
    {
        public enum Screen { Sell, ReceiveProduct, Product, Customer, User, Brand, Category, Color, Report, ShopInfo, Config, Claim, Return, Stock };
        public enum StatusIcon { None, Loading, Success, Info };

        public static UcSell UcSell;
        public static string ApiUrl = "http://api-test.powerdd.com";
        public static string ApiKey;
        public static bool ApiChecked;
        public static string LicenseKey;
        public static string CpuId;
        public static string ComputerName;
        public static string DatabaseName;
        public static string UserId;
        public static string DatabasePassword;
        public static string ShopId;
        public static string ShopName;
        public static string ShopParent;
        public static string ShopCustomer;
        public static string DevicePrefix;
        public static string DevicePrinter;
        public static string MemberType;
        public static string Code;
        public static SQLiteConnection SQLiteConnection;
        public const string SQLiteFileName = "System.dll";
        public static bool InitialFinished = false;

        public static string BarcodeNo;
        public static string status;


        public const string LoadingImageLocal = @"Resource/Images/Loading.gif";
        public const string LoadingImageUrl = "http://a.lnwpic.com/et1xpc.gif";
        public static string LogoPath;
        public const string LogoUrl = "https://lh3.googleusercontent.com/of2iTh9rSFHDQreN0Pu1CIV1_-K9BwqTyfFqNMkDtRA=w2655-no";

        public static string SelectCustomerId;
        public static string SelectCustomerName;
        public static string SelectCustomerSex;
        public static int SelectCustomerAge;
        public static int SelectCustomerSellPrice;


        public static string ProductId;
        public static string CategoryName;

        public static CloudStorageAccount AzureStorageAccount;
        public static CloudTableClient AzureTableClient;
        public static CloudTable AzureTable;


        public static UserControl UserControl;
        public static Panel MainPanel;
        //public static ToolStripStatusLabel lblStatus;
        public static Label lblStatus;
        public static int SelectedScreen = -1;
        public static int pageSize = 1100;
        public static int Records = 0;
        public static int RecordsCategory = 0;
        public static int Page  = 1;

        public static dynamic SystemConfig;
        public static dynamic UserGroup;

        public static UserEntity UserEntity;
        public static UserUpdateEntity UserUpdateEntity;
        public static List<UserEntity> UserEntityList;


        public static Hashtable ShopNameHashtable = new Hashtable();
        public static Hashtable CustomerNameHashtable = new Hashtable();


    }
}
