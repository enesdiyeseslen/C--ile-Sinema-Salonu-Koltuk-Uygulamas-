using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinemaSalonuProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SinemaKoltuklariniOlustur(14, 12, "A3,C3,E5-E7", "A4-A7,E2,H2,H3-H7,D2-D6");
        }

        private void SinemaKoltuklariniOlustur(int sutunSayisi, int satirSayisi, string bosKonumlar, string satilmisOlanlar)
        {
            // BosKonumlar ve SatilmisKoltuklar dizilerini parçaladım
            string[] bosKonumDizi = bosKonumlar.Split(',');

            // Satılmış koltukları işlemek için bir liste oluşturdum, burada satılmış olan koltukların bilgileri tutuldu
            List<string> satilmisOlanListe = new List<string>();

            // Satılmış koltukları ',' den sonrası için parçala işlemi:
            string[] satilmisOlanDizi = satilmisOlanlar.Split(','); //virgüle göre ayırarak.
            foreach (var item in satilmisOlanDizi)
            {
                if (item.Contains("-")) // - girilen aralıktaki koltukları ayarlamak için olan kısım:
                {
                    string[] aralik = item.Split('-'); //Her bir eleman içinde tire (-) karakterinin olup olmadığı kontrol ediliyor. ve aralık dizisine atılıyor.
                    char harf = aralik[0][0]; //aralik[0] ifadesi, aralik dizisinin ilk elemanını temsil eder. C3 yani mesela 
                                              //aralik[0][0], bu ilk elemanın ilk karakterini (harfini) temsil eder. mesela C
                                              //Bu harfi harf adlı char değişkenine atıyoruz.

                    int baslangic = int.Parse(aralik[0].Substring(1)); //aralik[0], başlangıç değerini temsil eder. mesla C3
                                                                       //.Substring(1), bu değerin ilk karakterini atlayarak geri kalanını alır. yani c3 ün 3ü.
                                                                       //Bu işlemin sonucunu baslangic adlı değişkene atıyoruz.

                    int bitis = int.Parse(aralik[1].Substring(1)); //aralik[1], bitiş değerini temsil eder. mesela aralik. mesela E5
                                                                   //.Substring(1), bu değerin ilk karakterini atlayarak geri kalanını alır. yani E5 ise 5 değerini tutuyor substring(1)
                                                                   //Bu işlemin sonucunu bitis adlı değişkene atadım.

                    for (int i = baslangic; i <= bitis; i++)
                    {
                        satilmisOlanListe.Add($"{harf}{i}"); // belirlenen başlangıç ve bitiş değerleri arasındaki tüm koltukları liste içine ekliyoruz.
                    }
                }
                else
                {
                    satilmisOlanListe.Add(item); //Eğer eleman aralık bilgisi içermiyorsa bu koltuğu direkt olarak listeye ekle.
                }
            }

            for (int j = 0; j < satirSayisi; j++)
            {
                for (int i = 0; i < sutunSayisi; i++)
                {
                    // Sıradaki harfi alarak butonlara atadım
                    string harf = ((char)('A' + i)).ToString();
                    Button button1 = new Button();
                    string buttonText = harf + (j + 1);

                    // BosKonumlar ve SatilmisKoltuklar listelerine göre renk ve görünüm ayarları:
                    if (bosKonumDizi.Contains(buttonText))
                    {
                        button1.BackColor = Color.Green;
                    }
                    else if (satilmisOlanListe.Contains(buttonText))
                    {
                        button1.Enabled = false;
                    }
                    else
                    {
                        button1.BackColor = Color.Green;
                    }
                    button1.Text = buttonText;
                    button1.Size = new Size(60, 30);
                    button1.Location = new Point(j * 70, i * 50);

                    // Yeşil butonun üzerine gelindiğinde mavi renk
                    button1.MouseEnter += (sender, e) => {
                        if (button1.BackColor == Color.Green)
                        {
                            button1.BackColor = Color.Blue;
                        }
                    };

                    // Yeşil butonun üzerinden çekildiğinde tekrar yeşil renk
                    button1.MouseLeave += (sender, e) => {
                        if (button1.BackColor == Color.Blue)
                        {
                            button1.BackColor = Color.Green;
                        }
                    };

                    // Yeşil butona tıklandığında kırmızı renk (seçildi), tekrar tıklanırsa yeşil renk
                    button1.Click += (sender, e) => {
                        if (button1.BackColor == Color.Blue)
                        {
                            button1.BackColor = Color.Red;
                        }
                        else if (button1.BackColor == Color.Red)
                        {
                            // Eğer buton kırmızı renkteyse (daha önce tıklanmış durumu)
                            // butonun rengini yeşile geri çevir
                            button1.BackColor = Color.Green; // Kırmızı olan buton tekrar tıklanırsa yeşile dönsün

                        }
                    };
                    // Kırmızı butonun üzerine gelindiğinde herhangi bir işlem yapılmayacak
                    button1.MouseEnter += (sender, e) => {
                        if (button1.BackColor == Color.Red)
                        {
                            // Herhangi bir işlem yok
                        }
                        else if (button1.BackColor == Color.Green)
                        {
                            button1.BackColor = Color.Blue;
                        }
                    };

                    // Kırmızı butonun üzerinden çekildiğinde herhangi bir işlem yapılmayacak
                    button1.MouseLeave += (sender, e) => {
                        if (button1.BackColor == Color.Red)
                        {
                            // Herhangi bir işlem yapılmdı
                        }
                        else if (button1.BackColor == Color.Blue)
                        {
                            button1.BackColor = Color.Green;
                        }
                    };

                    Controls.Add(button1);
                }
            }
        }

    }
}
