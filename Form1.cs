using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ders31LinqSorgularRestriction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbOkulEntities db = new DbOkulEntities();
        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Görünmesi İstenenler
            if (radioButton1.Checked == true)
            {
                Checked50denKucuk();
                //------------------------
                //var degerler = db.Tbl_Notlar.Where(x => x.Sinav1 < 50);
                //dataGridView1.DataSource = degerler.ToList();
                //Görünmesin1 
                //dataGridView1.Columns[9].Visible = false;
                //--------------------------------------------------
            }
            //--
            if (radioButton2.Checked == true)
            {
                AdiElifOlanlariGetir();

            }
            if (radioButton3.Checked == true)
            {
                AdiSoyadiTexttenAl();

            }
            //Linq Sorgular Select Projection (Yansıtma) Örnekleri
            if (radioButton4.Checked == true)
            {
                OgrenciSoyadiGetir();

            }
            if (radioButton5.Checked == true)
            {
                //Adı büyük soyadı küçük ve Ad Elif olmayanları Listele
                AdBuyukSoyadKucukSartliSecim();
            }
            // Linq Select Metodu Örnekleri
            if (radioButton6.Checked == true)
            {
                // SQL DE ORTALAMA VE DURUM İÇİN SORGULAR 
                /* --Update Tbl_Notlar set Ortalama=(Sinav1+Sinav2+Sinav3)/3; ==>> OrtalamaBul
                 
                 * -- DURUM ;
                 * --Update Tbl_Notlar set Durum=0 where Ortalama<50; -- Ortalama<50 olanlar false
                 * --update Tbl_Notlar set Durum=1 where Ortalama>=50;  --Ortalama>=50 olanlar true
                 */
                GectiKaldi();

            }
            if (radioButton7.Checked == true)
            {
                //TABLO BİRLEŞTİRME YAPILMALI 
                var query = db.Tbl_Notlar.Select(x =>
                  new
                  {
                      ÖĞRENCİID = x.OgrId,
                      ORTALAMA = x.Ortalama,
                      Durum = x.Durum == true ? "Geçti" : "Kaldı"

                  });
                dataGridView1.DataSource = query.ToList();
            }
            // 34.SelectMany(Projection) Kullanımı
            if (radioButton8.Checked == true)
            {
                GectiKaldi3();
            }
            if (radioButton9.Checked == true)
            {
                //Ders 35 Bölümleme Take Skip Then Kullanımları
                //-- TAKE -- Id ye gore sırala ilk 3 değeri al 
                Ilk3OgrenciyiAl();
            }
            if (radioButton10.Checked == true)
            {
                //-- TAKE -- Id ye gore sırala son 3 değeri al 
                Son3Ogrenci();

            }
            if (radioButton11.Checked == true)
            {
                //Ada göre sırala
                AdaGoreSirala();

            }
            if (radioButton12.Checked == true)
            {
                //İlk 5 degeri atla 
                Ilk5DegeriAtla();

            }
            if (radioButton13.Checked == true)
            {
                //36 Linq Sorgularda Group By Kullanımı ~ 1
                GroupBy1();
            }
            if (radioButton14.Checked==true)
            {
                GroupBy2();
            }

            if (radioButton15.Checked == true)
            {
                //Maximum ortalama
                label2.Text = db.Tbl_Notlar.Max(q => q.Ortalama).ToString();

            }
            if (radioButton16.Checked == true)
            {
                //minimum sınav 1 puanı
                label2.Text = db.Tbl_Notlar.Min(q => q.Sinav1).ToString();

            }

        }

        public void GroupBy2()
        {
            var query2 = from q1 in db.Tbl_Ogr
                         orderby q1.Sehir
                         group q1 by q1.Sehir into g
                         select new
                         {
                             Şehirler = g.Key,
                             Toplam = g.Count()
                         };
            dataGridView1.DataSource = query2.ToList();
        }

        public void GroupBy1()
        {
            var query = db.Tbl_Ogr.OrderBy(q => q.Sehir).GroupBy(q => q.Sehir).Select
                (x => new
                {
                    Şehirler = x.Key,
                    Toplam = x.Count()
                });
            dataGridView1.DataSource = query.ToList();
        }

        public void Ilk5DegeriAtla()
        {
            var query = db.Tbl_Ogr.OrderBy(x => x.Ogr_Id).Skip(5);
            dataGridView1.DataSource = query.ToList();
        }

        public void AdaGoreSirala()
        {
            var query = db.Tbl_Ogr.OrderBy(x => x.Ogr_Ad);
            dataGridView1.DataSource = query.ToList();
        }

        public void Son3Ogrenci()
        {
            var query = db.Tbl_Ogr.OrderByDescending(x => x.Ogr_Id).Take(3);
            dataGridView1.DataSource = query.ToList();
        }

        public void Ilk3OgrenciyiAl()
        {
            var query = db.Tbl_Ogr.OrderBy(x => x.Ogr_Id).Take(3);
            dataGridView1.DataSource = query.ToList();
        }

        public void GectiKaldi3()
        {
            var query = db.Tbl_Notlar.SelectMany(q => db.Tbl_Ogr.Where(y => y.Ogr_Id == q.OgrId),
                (x, y) => new
                {
                    Ad = y.Ogr_Ad,
                    Ortalama = x.Ortalama
                });
            dataGridView1.DataSource = query.ToList();
        }

        #region GeçtiKaldı()
        private void GectiKaldi()
        {
            var query = from x1 in db.Tbl_Notlar
                        join x2 in db.Tbl_Ogr
                        on x1.OgrId equals x2.Ogr_Id
                        select new
                        {
                            ÖğrenciID = x1.OgrId,
                            AD = x2.Ogr_Ad,
                            SOYAD = x2.Ogr_Soyad,
                            ORTALAMA = x1.Ortalama,
                            DURUM = x1.Durum == true ? "Geçti" : "Kaldı"
                        };
            dataGridView1.DataSource = query.ToList();
        }
        #endregion;
        public void AdBuyukSoyadKucukSartliSecim()
        {
            var query = db.Tbl_Ogr.Select(q => new //Anonymous
            {
                Adı = q.Ogr_Ad.ToUpper(),
                Soyadı = q.Ogr_Soyad.ToLower()
            }).Where(q => q.Adı != "Elif");
            dataGridView1.DataSource = query.ToList();
        }

        private void OgrenciSoyadiGetir()
        {
            var query = db.Tbl_Ogr.Select(q => q.Ogr_Soyad);
            dataGridView1.DataSource = query.ToList();
        }

        private void AdiSoyadiTexttenAl()
        {
            var adiSoyadi = txtAdSoyad.Text;

            var query = db.Tbl_Ogr.Where(q => q.Ogr_Ad == adiSoyadi || q.Ogr_Soyad == adiSoyadi);
            dataGridView1.DataSource = query.ToList();
        }

        public void AdiElifOlanlariGetir()
        {
            var isim = "Elif";
            var query = db.Tbl_Ogr.Where(q => q.Ogr_Ad.Contains(isim));
            //var query2 = db.Tbl_Ogr.Where(q => q.Ogr_Ad=="Elif"); // de olur!
            dataGridView1.DataSource = query.ToList();
        }

        public void Checked50denKucuk()
        {
            var degerler = from p1 in db.Tbl_Notlar
                           where (p1.Sinav1 < 50)
                           join p2 in db.Tbl_Ogr
                           on p1.OgrId equals p2.Ogr_Id
                           select new
                           {

                               ADI = p2.Ogr_Ad,
                               SOYADI = p2.Ogr_Soyad,
                               NOT = p1.Sinav1
                           };
            dataGridView1.DataSource = degerler.ToList();
        }
    }
}
