# Kütüphane Kitap Takip Yazılımı


* **Katmanlı mimari yapısı kullanılmıştır.**
* **Microsoft Access Database kullanılmıştır.** 

<br>

## Katmanlı Mimari Nedir ?

Katmanlı mimari             |  Bu projedeki katmanlı mimari şeması
:-------------------------:|:-------------------------:
 ![katmanlı mimari fotoğraf](https://drive.google.com/uc?export=view&id=1l_0DetYAh_VsJ4Vzv4ZiIgtFwHNFTdCJ) |  ![](https://drive.google.com/uc?export=view&id=1TQOM54mLihqtjLWEHranAsE9wQtQ0HYR)


Katmanlı mimari projelerimizi belirli bir standart ve düzene göre geliştirmemizi sağlayan, kodun okunabilirliğini arttıran, projelerimizin daha derli toplu olmasını sağlayan ve hata yönetimini daha kolay hale getiren bir yapıdır.

**Data Access Layer :** Bu katmanda sadece veritabanı işlemleri yapılmaktadır. Bu katmanın görevi veriyi ekleme, silme, güncelleme ve veritabanından çekme işlemidir. Bu katmanda bu işlemlerden başka herhangi bir işlem yapılmamaktadır.

**Business Layer :** Bu katmanda iş yüklerimizi yazıyoruz. Öncelikle şunu söylemeliyim bu katman Data Access tarafından projeye çekilmiş olan verileri alarak işleyecek olan katmandır. Biz uygulamalarımızda Data Access katmanını direk olarak kullanmayız. Araya Business katmanını koyarak bizim yerimize Business’ın yapmasını sağlarız. Kullanıcıdan gelen veriler öncelikle Business katmanına gider oradan işlenerek Data Access katmanına aktarılır. Bu katmanda ayrıca bu verilere kimlerin erişeceğini belirtiyoruz. Örneğin IT ve Muhasebe bölümü var. IT bölümünün veri tabanına ekleme işlemleri yapmasını istiyoruz ama Muhasebe bölümünün sadece verileri çekmesini istiyorsak bunu Business Katmanında gerçekleştiriyoruz.

**Presentation Layer:** Bu katman kullanıcı ile etkileşimin yapıldığı katmandır. Burası Windows form da olabilir, Web’te olabilir veya Bir Consol uygulamasida olabilir. Burada temel amac kullaniciya verileri göstermek ve kullanıcıdan gelen verileri Business Katmanı ile Data Access’e iletmektir.

**Entities :** Arkadaşlar temeldeki 3 katmanı inceledik. Entities Katmanında ise genelde domain olarak adlandırılan classlarımızı tanımlıyoruz. Bu Entities katmanının ismini domain olarakta değiştirebilirsiniz veya Common katmanıda yapabilirsiniz. Ben Entities ismini tercih ediyorum. Bu katmanda proje boyunca kullanacağımız ana classlarımızı belirliyoruz yani gerçek nesnelerimizi belirlediğimiz yer burası. Daha anlaşılabilir bir şekilde anlatmak için birkaç örnek vereyim. Örneğin bir Stok veritabını sistemi yapmak istiyorsunuz. Bu sistemde Ürün bilgileriniz, Kategori bilgileriniz ve Satış bilgilerinizin olduğunu varsayalım. İşte bu bilgilerinizi burada tanımlıyorusunuz. Ürün classınız içerisinde Property olarak ÜrünAdı, ÜrünID si, ÜrünStokMiktari gibi, ÜrünFiyatı gibi propertyler olabilir. Bu katmanı hem Data Access hem Business hemde Presentation katmanı kullanmaktadır.

-------------------------------------------------------

## Program İçi Görüntüler

**Ana Menü**

![katmanlı mimari fotoğraf](https://drive.google.com/uc?export=view&id=1UEnplN3xr529eZ5SRpKEd4PCiXg60hSH)

**Öğrenci Penceresi**         |  **Kütüphane İstatistik Penceresi**
:-------------------------:|:-------------------------:
![](https://drive.google.com/uc?export=view&id=11eL0ROGJNOoa4UlSFwxUdEZ3AOfJFe-Q)  |  ![](https://drive.google.com/uc?export=view&id=1Zl38i_xMULxc1giEwvegezYsuK2HIGr1)


**Kitap Penceresi**         |  **Database Diyagramı**
:-------------------------:|:-------------------------:
![](https://drive.google.com/uc?export=view&id=15o8UVLX3Ngd1aCmVFmILPwijzB1KYV-c)  |  ![](https://drive.google.com/uc?export=view&id=17QMUDT5zmuT4XKHTLoq2vypl23gjdRDd)

**Öğrenci Kitap Teslim-İade Penceresi**

![katmanlı mimari fotoğraf](https://drive.google.com/uc?export=view&id=142pWsbgBM50ODslKtxkCAnJYsN3H8bU2)

------------------------------------------------------

### Kurulum
[Bu linkten](https://drive.google.com/file/d/1qjv8eTM4m7whCT78r_yg_VhatHB6iCDF/view?usp=sharing){:target="_blank"} "Kütüphane Setup.msi" dosyasını indirerek kurulumu gerçekleştirebilirsiniz. 

### KAYNAKÇA

* https://medium.com/kodcular/katmanl%C4%B1-mimari-9fb34ef8c376#:~:text=Katmanl%C4%B1%20mimari%20projelerimizi%20belirli%20bir,kolay%20hale%20getiren%20bir%20yap%C4%B1d%C4%B1r.


