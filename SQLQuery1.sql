-- Seferler tablosunu oluşturuyoruz
CREATE TABLE Seferler (
    SeferID INT PRIMARY KEY IDENTITY,          -- Otomatik artan birincil anahtar
    SeferNumarasi NVARCHAR(50),                -- Sefer numarası
    KalkisSehri NVARCHAR(50),                  -- Kalkış şehri
    VarisSehri NVARCHAR(50),                   -- Varış şehri
    KalkisSaati DATETIME                       -- Kalkış saati (Doğru veri türü kullanıldı)
);

-- Yolcular tablosunu oluşturuyoruz
CREATE TABLE Yolcular (
    YolcuID INT PRIMARY KEY IDENTITY,          -- Otomatik artan birincil anahtar
    SeferID INT,                               -- Sefer ile ilişkilendirme için dış anahtar
    YolcuAdi NVARCHAR(50),                     -- Yolcu adı
    TelefonNumarasi NVARCHAR(50),              -- Telefon numarası
    Cinsiyet NVARCHAR(10),                     -- Cinsiyet
    KoltukNumarasi NVARCHAR(10),               -- Koltuk numarası
    FOREIGN KEY (SeferID) REFERENCES Seferler(SeferID) ON DELETE CASCADE
);
