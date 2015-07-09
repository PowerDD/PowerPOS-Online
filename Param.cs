using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPOS_Online
{
    public class Param
    {
        public enum Screen { Sell, ReceiveProduct, Product, Customer, User, Brand, Category, Color, Report, ShopInfo, Config, Claim };

        public static string apiUrl;
        public static string apiKey;
        public static bool apiChecked;
        public static string licenseKey;
        public static string cpuId;
        public static string computerName;
        public static string databaseName;
        public static string databasePassword;
        public static string shopId;
        public static string shopName;

        public static string SelectCustomerId;
        public static string SelectCustomerName;
        public static int SelectCustomerSex;
        public static int SelectCustomerAge;


        public static CloudStorageAccount azureStorageAccount;
        public static CloudTableClient azureTableClient;
        public static CloudTable azureTable;


        public static UserControl userControl;
        public static Panel mainPanel;
        public static ToolStripStatusLabel lblStatus;
        public static int selectedScreen = -1;

        public static dynamic systemConfig;
        public static dynamic userGroup;

        public static UserEntity userEntity;
        public static UserUpdateEntity userUpdateEntity;
        public static List<UserEntity> userEntityList;


    }
}
