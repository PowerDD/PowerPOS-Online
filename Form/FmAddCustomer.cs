using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThaiNationalIDCard;

namespace PowerPOS_Online
{
    public partial class FmAddCustomer : Form
    {
        private static Bitmap _PHOTO;

        public FmAddCustomer()
        {
            InitializeComponent();
            LoadData();
        }

        private void FmAddCustomer_Load(object sender, EventArgs e)
        {
            //LoadData();
            if (Param.SystemConfig.SellPrice == null)
            {
                cbbSellPrice.Items.Clear();
                cbbSellPrice.Items.Add("ปลีก");
                cbbSellPrice.Items.Add("ส่ง1");
                cbbSellPrice.Items.Add("ส่ง2");
                cbbSellPrice.SelectedIndex = 0;
            }
        }

        private void LoadData()
        {
            for (int i = 0; i <= 50; i++)
            {
                cbbDcPercent.Items.Add(i + " %");
            }
            cbbDcPercent.SelectedIndex = 0;

            for (int i = 0; i <= 90; i++)
            {
                cbbCredit.Items.Add(i + " วัน");
            }
            cbbCredit.SelectedIndex = 0;

            cbbSellPrice.SelectedIndex = 0;

            DataTable dt = Util.DBQuery(@"SELECT Name FROM Province ORDER BY ID*1");
            cbbProvince.Items.Clear();
            cbbProvince.Items.Add("จังหวัด");
            cbbProvinceS.Items.Clear();
            cbbProvinceS.Items.Add("จังหวัด");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbbProvince.Items.Add(dt.Rows[i]["name"].ToString());
                cbbProvinceS.Items.Add(dt.Rows[i]["name"].ToString());
            }
            cbbProvince.SelectedIndex = 0;
            cbbProvinceS.SelectedIndex = 0;
        }

        private void btnSmartCard_Click(object sender, EventArgs e)
        {
            btnSmartCard.Enabled = false;
            ThaiIDCard idcard = new ThaiIDCard();
            Personal personal = idcard.readAll();
            Personal personal_photo = idcard.readAllPhoto();

            if (personal != null)
            {
                btnSelectImage.Visible = false;
                txtCitizenId.Text = personal.Citizenid;
                txtName.Text = personal.Th_Firstname;
                txtLastname.Text = personal.Th_Lastname;

                rdbMan.Checked = personal.Sex == "1";
                rdbWoman.Checked = personal.Sex != "1";
                cbbProvince.SelectedItem = personal.addrProvince.Replace("จังหวัด", "");
                //lblProvince.Text = personal.addrAmphur + " " + personal.addrProvince  personal.addrTambol;

                string[] address = personal.Address.Replace(personal.addrTambol, "").Replace(personal.addrAmphur, "").Replace(personal.addrProvince, "").Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sb = new StringBuilder(address[0]);
                for (int i = 1; i < address.Length; i++)
                {
                    sb.Append(" " + address[i]);
                }
                txtAddress1.Text = sb.ToString();

                txtSubDistrict.Text = personal.addrTambol.Replace("แขวง", "").Replace("ตำบล", "");
                cbbDistrict.SelectedItem = personal.addrAmphur.Replace("เขต", "").Replace("อำเภอ", "");

                //txtAddress.Text = personal.Address.Replace(personal.addrTambol, "").Replace(personal.addrAmphur, "").Replace(personal.addrProvince, "").Trim();
                //txtSubDistrict.Text = personal.addrTambol;
                //dtBirthday.Value = personal.Birthday;
                //dtIssue.Value = personal.Issue;
                //dtExpire.Value = personal.Expire;
                _PHOTO = personal_photo.PhotoBitmap;
                ptbPhoto.Image = personal_photo.PhotoBitmap;

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                dtpBarthday.Text = personal.Birthday.ToString("d MMMM yyyy");

                LoadCustomerData(sender, e, "CitizenID", txtCitizenId.Text.Trim());
            }
            else
            {
                MessageBox.Show("ไม่สามารถอ่านข้อมูลได้\nกรุณาติดตั้งเครื่องอ่าน Smart Card ให้เรียบร้อย\nแล้วลองใหม่อีกครั้ง", "มีข้อผิดพลาดเกิดขึ้น", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnSmartCard.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtName.Text.Trim() == "")
            {
                txtName.Focus();
            }
            else if (txtLastname.Text.Trim() == "")
            {
                txtLastname.Focus();
            }
            else if (txtMobile.Text.Trim() == "")
            {
                txtMobile.Focus();
            }
            else
            {
                var insert = true;
                if ((txtMobile.Text.Trim() != "" && txtMobile.Text.Trim().Length == 10) || txtCitizenId.Text.Trim() != "" && txtCitizenId.Text.Trim().Length == 13)
                {
                    var column = "Mobile";
                    var data = txtMobile.Text.Trim();
                    if (txtCitizenId.Text.Trim() != "" && txtCitizenId.Text.Trim().Length == 13)
                    {
                        column = "CitizenID";
                        data = txtCitizenId.Text.Trim();
                    }
                    DataTable dt = Util.DBQuery(@"SELECT COUNT(*) cnt FROM Customer WHERE " + column + " = '" + data + "'");
                    insert = dt.Rows[0]["cnt"].ToString() == "0";
                    if (!insert)
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                        Util.DBExecute(string.Format(@"UPDATE Customer SET Firstname = '{0}', Lastname = '{1}', Nickname = '{2}', Sex = '{3}', Birthday = '{4}',
                        CitizenID = '{5}', CardNo = '{6}', Mobile = '{7}', Email = '{8}', Address = '{9}', Address2 = '{10}', SubDistrict = '{11}', District = '{12}', Province = '{13}', Zipcode = '{14}',
                        ShopName = '{15}', ShopSameAddress = {16}, ShopAddress = '{17}', ShopAddress2 = '{18}', ShopSubDistrict = '{19}', ShopDistrict = '{20}', ShopProvince = '{21}', ShopZipcode = '{22}',
                        UpdateDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), UpdateBy = '{23}', SellPrice = {24}, Credit = {25}, Sync = 1
                        WHERE Mobile = '{7}'",
                            txtName.Text.Trim(), txtLastname.Text.Trim(), txtNickName.Text.Trim(), (rdbMan.Checked) ? "M" : ((rdbWoman.Checked) ? "F" : "null"), dtpBarthday.Value.ToString("yyyy-MM-dd"),
                            txtCitizenId.Text.Trim(), txtCardId.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(),
                            txtSubDistrict.Text.Trim(),
                            cbbDistrict.SelectedItem == null ? "" : cbbDistrict.SelectedItem.ToString(),
                            cbbProvince.SelectedItem.ToString() == "จังหวัด" ? "" : cbbProvince.SelectedItem.ToString(), txtZipCode.Text.Trim(),
                            txtShopName.Text.Trim(), (cbxSameAddress.Checked) ? 1 : 0, txtAddressS1.Text.Trim(), txtAddressS2.Text.Trim(), txtSubDistrictS.Text.Trim(),
                            cbbDistrictS.SelectedItem == null ? "" : cbbDistrictS.SelectedItem.ToString(),
                            cbbProvinceS.SelectedItem.ToString() == "จังหวัด" ? "" : cbbProvinceS.SelectedItem.ToString(), txtZipCodeS.Text.Trim(), Param.UserId,
                            cbbSellPrice.SelectedItem.ToString().Replace("ปลีก", "0").Replace("ส่ง", ""),
                            cbbCredit.SelectedItem.ToString().Replace(" วัน", "")
                        ));
                    }
                }

                if (insert)
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    Util.DBExecute(string.Format(@"INSERT INTO Customer (ID, Firstname, Lastname, Nickname, Sex, Birthday, 
                    CitizenID, CardNo, Mobile, Email, Address, Address2, SubDistrict, District, Province, Zipcode,
                    ShopName, ShopSameAddress, ShopAddress, ShopAddress2, ShopSubDistrict, ShopDistrict, ShopProvince, ShopZipcode, AddDate, AddBy, SellPrice, Credit, Sync) VALUES (
                    (   SELECT IFNULL('{24}'||SUBSTR('000000'||(SUBSTR(MAX(ID), 2, 6)+1), -6, 6), '{24}000001') ID
                        FROM Customer
                        WHERE SUBSTR(ID, 1, 1) = '{24}'),
                    '{0}', '{1}', '{2}', '{3}', '{4}',
                    '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}',
                    '{15}', {16}, '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), '{23}', {25}, {26}, 1)",
                        txtName.Text.Trim(), txtLastname.Text.Trim(), txtNickName.Text.Trim(), (rdbMan.Checked) ? "M" : ((rdbWoman.Checked) ? "F" : "null"),
                        dtpBarthday.Value.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd") ? "" : dtpBarthday.Value.ToString("yyyy-MM-dd"),
                        txtCitizenId.Text.Trim(), txtCardId.Text.Trim(), txtMobile.Text.Trim(), txtEmail.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(),
                        txtSubDistrict.Text.Trim(),
                        cbbDistrict.SelectedItem == null ? "" : cbbDistrict.SelectedItem.ToString(),
                        cbbProvince.SelectedItem.ToString() == "จังหวัด" ? "" : cbbProvince.SelectedItem.ToString(), txtZipCode.Text.Trim(),
                        txtShopName.Text.Trim(), (cbxSameAddress.Checked) ? 1 : 0, txtAddressS1.Text.Trim(), txtAddressS2.Text.Trim(), txtSubDistrictS.Text.Trim(),
                        cbbDistrictS.SelectedItem == null ? "" : cbbDistrictS.SelectedItem.ToString(),
                        cbbProvinceS.SelectedItem.ToString() == "จังหวัด" ? "" : cbbProvinceS.SelectedItem.ToString(), txtZipCodeS.Text.Trim(), Param.UserId, Param.DevicePrefix,
                        cbbSellPrice.SelectedItem.ToString().Replace("ปลีก", "0").Replace("ส่ง", ""),
                        cbbCredit.SelectedItem.ToString().Replace(" วัน", "")
                    ));
                }

                if (_PHOTO != null)
                {
                    string path = Directory.GetCurrentDirectory();
                    //if (!Directory.Exists(path + @"/Resources")) Directory.CreateDirectory(path + "/Resources");
                    //if (!Directory.Exists(path + @"/Resources/Images")) Directory.CreateDirectory(path + @"/Resources/Images");
                    if (!Directory.Exists(path + @"/Resources/Images/Customer")) Directory.CreateDirectory(path + @"/Resources/Images/Customer");
                    using (Bitmap tempImage = new Bitmap(_PHOTO))
                    {
                        var name = path + @"/Resources/Images/Customer/" + txtCitizenId.Text.Trim() + ".jpg";
                        if (File.Exists(name))
                            File.Delete(name);
                        tempImage.Save(name, ImageFormat.Jpeg);
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        private void cbxSameAddress_CheckedChanged(object sender, EventArgs e)
        {
            txtAddressS1.Enabled = !cbxSameAddress.Checked;
            txtAddressS2.Enabled = !cbxSameAddress.Checked;
            txtSubDistrictS.Enabled = !cbxSameAddress.Checked;
            cbbDistrictS.Enabled = !cbxSameAddress.Checked;
            cbbProvinceS.Enabled = !cbxSameAddress.Checked;
            txtZipCodeS.Enabled = !cbxSameAddress.Checked;
            if (cbxSameAddress.Checked)
            {
                txtAddressS1.Text = txtAddress1.Text.Trim();
                txtAddressS2.Text = txtAddress2.Text.Trim();
                txtSubDistrictS.Text = txtSubDistrict.Text.Trim();
                txtZipCodeS.Text = txtZipCode.Text.Trim();
                if (cbbProvinceS.SelectedIndex != cbbProvince.SelectedIndex)
                {
                    cbbProvinceS.SelectedIndex = cbbProvince.SelectedIndex;
                    cbbProvinceS_SelectedIndexChanged(sender, e);
                    cbbDistrictS.SelectedIndex = cbbDistrict.SelectedIndex;
                }
                if (cbbDistrictS.SelectedIndex != cbbDistrict.SelectedIndex)
                {
                    cbbDistrictS.SelectedIndex = cbbDistrict.SelectedIndex;
                }
                cbbDistrictS.Enabled = false;
                cbbProvinceS.Enabled = false;
                txtZipCodeS.Enabled = false;
            }
            CheckInputData(sender, e);
        }

        private void CheckInputData(object sender, KeyEventArgs e)
        {
            btnSave.Enabled = txtName.Text.Trim() != "" && txtLastname.Text.Trim() != "" && txtMobile.Text.Trim() != "";
        }

        private void cbbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbDistrict.Items.Clear();
            if (cbbProvince.SelectedIndex == 0)
            {
                cbbDistrict.Enabled = false;
                txtZipCode.Text = "";
                txtZipCode.Enabled = false;
            }
            else
            {
                DataTable dt = Util.DBQuery(@"SELECT Name FROM District WHERE Province = (SELECT ID FROM Province WHERE Name = '" + cbbProvince.SelectedItem.ToString() + "') ORDER BY ID*1");
                cbbDistrict.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbbDistrict.Items.Add(dt.Rows[i]["Name"].ToString());
                }
                cbbDistrict.SelectedIndex = 0;
                cbbDistrict.Enabled = true;
                txtZipCode.Enabled = true;
            }
            CheckInputData(sender, e);
        }

        private void cbbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = Util.DBQuery(@"SELECT Zipcode FROM District WHERE Province = (SELECT ID FROM Province WHERE Name = '" +
                cbbProvince.SelectedItem.ToString() + "') AND Name = '" + cbbDistrict.SelectedItem.ToString() + "'");
            txtZipCode.Text = dt.Rows[0]["Zipcode"].ToString();
            CheckInputData(sender, e);
        }

        private void cbbProvinceS_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbDistrictS.Items.Clear();
            if (cbbProvinceS.SelectedIndex == 0)
            {
                cbbDistrictS.Enabled = false;
                txtZipCodeS.Text = "";
                txtZipCodeS.Enabled = false;
            }
            else
            {
                DataTable dt = Util.DBQuery(@"SELECT Name FROM District WHERE Province = (SELECT ID FROM Province WHERE Name = '" + cbbProvinceS.SelectedItem.ToString() + "') ORDER BY ID*1");
                cbbDistrictS.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbbDistrictS.Items.Add(dt.Rows[i]["Name"].ToString());
                }
                cbbDistrictS.SelectedIndex = 0;
                cbbDistrictS.Enabled = true;
                txtZipCodeS.Enabled = true;
            }
            CheckInputData(sender, e);
        }

        private void cbbDistrictS_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = Util.DBQuery(@"SELECT Zipcode FROM District WHERE Province = (SELECT ID FROM Province WHERE Name = '" +
                cbbProvinceS.SelectedItem.ToString() + "') AND Name = '" + cbbDistrictS.SelectedItem.ToString() + "'");
            txtZipCodeS.Text = dt.Rows[0]["Zipcode"].ToString();
            CheckInputData(sender, e);
        }

        private void txtMobile_TextChanged(object sender, EventArgs e)
        {
            if (txtMobile.Text.Trim().Length == 10)
            {
                LoadCustomerData(sender, e, "Mobile", txtMobile.Text.Trim());
            }
        }

        public void LoadCustomerData(object sender, EventArgs e, string dataType, string keyword)
        {
            DataTable dt = Util.DBQuery(@"SELECT * FROM Customer WHERE " + dataType + " = '" + keyword + "'");
            if (dt.Rows.Count != 0)
            {
                var row = dt.Rows[0];
                txtMobile.Text = row["Mobile"].ToString();
                txtName.Text = row["Firstname"].ToString();
                txtLastname.Text = row["Lastname"].ToString();
                txtNickName.Text = row["Nickname"].ToString();
                rdbMan.Checked = row["Sex"].ToString() == "M";
                rdbWoman.Checked = row["Sex"].ToString() == "F";
                if (row["Birthday"].ToString() != "")
                    dtpBarthday.Value = Convert.ToDateTime(row["Birthday"].ToString());
                txtCitizenId.Text = row["CitizenID"].ToString();
                txtCardId.Text = row["CardNo"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtAddress1.Text = row["Address"].ToString();
                txtAddress2.Text = row["Address2"].ToString();
                txtSubDistrict.Text = row["SubDistrict"].ToString();
                cbbProvince.SelectedIndex = cbbProvince.FindString(row["Province"].ToString());
                cbbProvince_SelectedIndexChanged(sender, e);
                cbbDistrict.SelectedIndex = cbbDistrict.FindString(row["District"].ToString());
                txtZipCode.Text = row["Zipcode"].ToString();
                txtShopName.Text = row["ShopName"].ToString();
                txtAddressS1.Text = row["ShopAddress"].ToString();
                txtAddressS2.Text = row["ShopAddress2"].ToString();
                txtSubDistrictS.Text = row["ShopSubDistrict"].ToString();
                cbbProvinceS.SelectedIndex = cbbProvinceS.FindString(row["ShopProvince"].ToString());
                cbbProvinceS_SelectedIndexChanged(sender, e);
                cbbDistrictS.SelectedIndex = cbbDistrictS.FindString(row["ShopDistrict"].ToString());
                txtZipCodeS.Text = row["ShopZipcode"].ToString();
                cbxSameAddress.Checked = row["ShopSameAddress"].ToString() == "True";
                cbbSellPrice.SelectedIndex = row["SellPrice"].ToString() == "0" ? 0 : cbbSellPrice.FindString("ส่ง " + row["SellPrice"].ToString());
                cbbCredit.SelectedIndex = cbbCredit.FindString(row["Credit"].ToString() + " วัน");
            }
        }

        private void CheckInputData(object sender, EventArgs e)
        {
            btnSave.Enabled = txtName.Text.Trim() != "" && txtLastname.Text.Trim() != "" && txtMobile.Text.Trim() != "";
        }


    }
}
