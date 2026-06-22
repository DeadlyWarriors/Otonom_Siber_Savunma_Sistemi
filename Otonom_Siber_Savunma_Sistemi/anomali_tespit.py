import os
import subprocess
import time
from scapy.all import sniff, IP
import pandas as pd
from sklearn.svm import OneClassSVM

normal_veri = pd.DataFrame({
    'paket_sayisi': [10, 15, 12, 8, 14, 11],
    'farkli_ip_sayisi': [2, 3, 2, 1, 3, 2]
})

model = OneClassSVM(kernel="rbf", gamma=0.1, nu=0.05)
model.fit(normal_veri)

kronometre = time.time()
paket_sepeti = []

def paket_yakala(paket):
    if paket.haslayer(IP):
        paket_sepeti.append(paket[IP].src) 

def trafik_analizi():
    global kronometre, paket_sepeti

    if time.time() - kronometre > 3:
        paket_sayisi = len(paket_sepeti)
        farkli_ip_sayisi = len(set(paket_sepeti)) 
        
        anlik_durum = pd.DataFrame([[paket_sayisi, farkli_ip_sayisi]], columns=['paket_sayisi', 'farkli_ip_sayisi'])
        tahmin = model.predict(anlik_durum)

        paket_sepeti = []
        kronometre = time.time()

        dosya_yolu = r"C:\Users\demir\OneDrive\Masaüstü\Güvenli_silme\Güvenli_silme\Program.cs"

        if tahmin[0] == -1 and paket_sayisi > 0:
            print(f"[!] Tehdit Algılandı!!! Paket Sayısı: {paket_sayisi}, Farklı IP: {farkli_ip_sayisi}")
            os.system(f"dotnet run {dosya_yolu}")
            return False 
            
    return True 

print("[+] Otonom Siber Savunma Sistemi Başlatıldı. Ağ dinleniyor...")

sniff(prn=paket_yakala, stop_filter=lambda p: not trafik_analizi())