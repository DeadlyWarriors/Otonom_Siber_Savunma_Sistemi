using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Security.Cryptography;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;


class Program()
{
    public static void Main(string[] args)
    {
        string masaustuYolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string hedefDosya = Path.Combine(masaustuYolu, "mrrobot.txt");

        if (File.Exists(dosya))
        {
            Console.WriteLine("[+] Dosya imha işlemi başlatılıyor... ");
            bozma(dosya);
        }
        else 
        { 
            Console.WriteLine("[-] Dosya bulunamadı... ");
        }
        Console.ReadLine();
    }



    public static void bozma(string dosya)
    {
        using (FileStream fs = new FileStream(dosya, FileMode.Open, FileAccess.Write, FileShare.None))
        {
            long dosyaboyutu = fs.Length;
            byte[] byteveri = new byte[dosyaboyutu];

            RandomNumberGenerator.Fill(byteveri);

            fs.Position = 0;

            fs.Write(byteveri, 0, byteveri.Length);

            fs.Flush(true);
            Console.WriteLine("[+] Dosya içeriği yok edildi... ");
            tarih_degistirme(dosya);
            Console.WriteLine("[+] Dosya zamanları değiştirildi... ");

            if (!OperatingSystem.IsWindows())
            {
                Console.WriteLine("[-] Windowsa özgü erişim değişikliğini atlama");
            }
        }

        if (OperatingSystem.IsWindows())
        {
            var kalkan = new FileSecurity();
            var kurallar = new FileSystemAccessRule(
                "Everyone",
                FileSystemRights.ReadExtendedAttributes | FileSystemRights.ReadAttributes,
                AccessControlType.Deny);
            kalkan.AddAccessRule(kurallar);

            new FileInfo(dosya).SetAccessControl(kalkan);
            Console.WriteLine("[+] Dosya erişim izinleri değiştirildi... ");
        }

        File.Delete(dosya);
        Console.WriteLine("[+] Dosya imha edildi... ");
    }

    public static void tarih_degistirme(string dosya)
    {
        Random rd = new Random();
        int yıl = rd.Next(2020, 2030);   
        int ay = rd.Next(1, 13);       
        int gün = rd.Next(1, 29);         
        int saat = rd.Next(0, 24);        
        int dakika = rd.Next(0, 60);      
        int saniye = rd.Next(0, 60);
        File.SetCreationTime(dosya, new DateTime(2020, 1, 1));
        DateTime randomDate = new DateTime(yıl, ay, gün, saat, dakika, saniye);
        File.SetLastWriteTime(dosya, randomDate);
    }    
}