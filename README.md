🛡️ Otonom Siber Savunma Sistemi - README
Bu proje, yapay zeka ile siber savunmayı birleştiren otonom bir güvenlik aracıdır. Sistemin arka planda yaptığı iş tam olarak şudur:

1 - Ağ Kartını Canlı İzler: Scapy kütüphanesi yardımıyla bilgisayarınızın ağ kartını sürekli dinler ve havada uçuşan tüm IP paket verilerini (gelen/giden internet trafiğini) anlık olarak yakalar.

2 - Yapay Zeka ile Saldırı Analizi Yapar: Toplanan ağ verilerini (paket sayıları ve benzersiz IP adreslerini) 3 saniyede bir One-Class SVM yapay zeka modeline fırlatır. Model, trafiğin normal bir kullanım mı yoksa bir anomali (DDoS saldırısı, port taraması vb.) mi olduğunu analiz eder.

3 - Tehdit Algılandığında Tetiklenir: Yapay zeka bir saldırı tespit ettiği an, ağ dinleme nöbetçisini (sniff) bıçak gibi keserek durdurur.

4 - C# Motoru ile Kritik Veriyi İmha Eder: Python scripti durduğu saniyede, işletim sisteminin subprocess mimarisini kullanarak arka plandaki C# kodunu (dotnet run) ateşler. C# motoru da masaüstünde veya kök dizinde bulunan, ele geçirilmesini istemediğiniz kritik bir dosyayı (mrrobot.txt) zırhlı ve güvenli bir şekilde diskten tamamen siler.
