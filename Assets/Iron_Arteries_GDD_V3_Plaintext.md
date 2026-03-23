# PROJECT: IRON ARTERIES (Demir Damarlar) — GDD V3 Düzyazı Formatı

## Oyunun Özeti ve Vizyonu

Project: Iron Arteries (Demir Damarlar), Soğuk Savaş ve Modern Savaş temalı, Grand Strateji soslu bir Gerçek Zamanlı Strateji (RTS) oyunudur. Oyunun temel odağı lojistik ağ yönetimi ve tedarik zinciri savaşıdır. Klasik RTS oyunlarındaki "işçi ile maden topla" mantığı yerine, makro düzeyde bir otomatize lojistik ağı kurmaya odaklanır. Oyunda savaşları en büyük ordu değil, tedarik zincirini en iyi koruyan kazanır. Tasarım felsefesi şudur: savaşı kazanmak için düşmanın birliklerini değil, lojistik damarlarını kes; bir ordu ancak beslenirse ayakta kalabilir. Not olarak, orijinal belgede yer alan Trenler ve Spec-Ops mekanikleri tasarım kararıyla projeden tamamen çıkarılmıştır.

## Temel Oyun Döngüsü (Core Loop)

Oyunun saniyeden saniyeye ve dakikadan dakikaya akışı beş temel adımdan oluşur. Birinci adım "Genişle"dir: İnşaat Mangası ile serbestçe binalar inşa edilir, kaynak alanlarının üzerine fabrikalar kurulur ve depolar ile alan kontrolü (AoE) sağlanır. Bu adımın girdisi ham kaynaklar ve inşaat mangası, çıktısı fabrikalar ve depolardır. İkinci adım "Ağ Kur"dur: Bölge depoları inşa edilir ve lojistik ağını hızlandırmak için yollar yükseltilir (toprak yoldan asfalta). Girdisi fabrikalar ve depolar, çıktısı lojistik hatları ve yollardır. Üçüncü adım "Fonu Yönet"tir: Arayüzden kaynakların yüzde kaçının Savaş Fonu (Market) için satılacağı, yüzde kaçının Ana Depoya gideceği belirlenir. Girdisi lojistik ağı çıktıları, çıktısı Savaş Fonu ve depolanmış kaynaklardır. Dördüncü adım "Savaş ve Besle"dir: Zırhlı taşıyıcılarla askerler cepheye sürülür, İleri Karakollar (FOB) kurularak cephe mühimmatsız ve erzaksız bırakılmaz. Girdisi birlikler, mühimmat ve erzak; çıktısı cephe kontrolüdür. Beşinci adım "Sabote Et"tir: Düşmanın lojistik hatlarına, market kamyonlarına ve depolarına saldırılarak ekonomisi felç edilir. Girdisi saldırı birlikleri, çıktısı düşman ekonomisi zararıdır. Beşinci adımdan sonra döngü tekrar birinci adıma döner.

## Ekonomi ve Lojistik Sistemi

Oyunun kalbini oluşturan bu sistem, işçi birimlerinin olmadığı otomatize bir yapıdadır.

### Kaynaklar

Oyunda beş temel kaynak türü bulunur. Petrol, araç hareketliliği ve lojistik ağı için kullanılır ve önceliği yüksektir. Demir/Metal, zırhlı araç ve bina inşaatı için kullanılır ve önceliği yüksektir. Nadir Madenler, radar, ileri teknoloji ve hava kuvvetleri için kullanılır ve önceliği ortadır. Erzak, piyade mangalarının hayatta kalması için kullanılır ve önceliği kritiktir. Savaş Fonu ise market ticareti, özel sektör üretimi ve teknoloji araştırmaları için kullanılır ve önceliği değişkendir.

### Kaynak Akış Zinciri

Kaynakların üretimden tüketime geçişi beş aşamadan oluşur. Birinci aşama çıkarımdır: ham kaynak haritadaki madenden veya kuyudan çıkarılır, bu kaynak alanında gerçekleşir. İkinci aşama depolamadır: ham kaynak en yakın deponun AoE alanına aktarılır. Üçüncü aşama işlemedir: fabrikalar depo AoE içinden kaynağı sanal olarak çeker ve işler. Dördüncü aşama dağıtımdır: işlenmiş kaynak Madde Deposuna aktarılır veya Markete satılır. Beşinci aşama tüketimdir: birlikler ve yapılar işlenmiş kaynağı kullanır.

### Üretim Mekanikleri — Sanal Üretim (AoE Sistemi)

Oyunda klasik bölge mantığı yoktur. Bunun yerine her deponun belirli bir kapsama alanı (Area of Effect — AoE) vardır. Ana Depoya gelen baz kaynak, AoE içindeki fabrikalar tarafından sanal olarak (çevresinde kamyon görmeden) çekilir. İşlenen malzeme Madde Deposuna aktarılır. Madde Deposunun AoE'sindeki diğer fabrikalar da bu işlenmiş kaynağı sanal olarak çeker. Bu sistem, üretimi otomatize ederken lojistik karmaşıklığı depo-fabrika yerleşimine taşır. Önemli kural: AoE içindeki taşıma kamyonsuz ve otomatiktir; stratejik derinlik, depoların nereye konulduğundadır.

### Üretim Mekanikleri — Tesis Odaklı Kamyonlar

Depo alanı dışındaki tesisler (kendi başına kurulan uzak madenler) farklı çalışır. Bu tesisler kendi içlerinden spesifik kamyonlar üretir. Kamyonlar, belirlenen depoya aktarmasız ve doğrudan taşıma yapar. Bu kamyonlar düşman saldırısına açıktır ve lojistik hattın en savunmasız halkalarıdır. AoE içi (sanal) taşıma otomatik ve görünmezdir, hızı anlıktır, saldırıya kapalıdır ve maliyeti sadece depo/fabrika inşasıdır; depo yıkılırsa üretim durur. AoE dışı (kamyon) taşıma ise fiziksel kamyonla haritada görünür şekilde yapılır, hızı mesafeye ve yola bağlıdır, pusuya açıktır, kamyon üretim ve yakıt tüketimi maliyeti vardır; hat kesilirse kamyonlar mahsur kalır.

### Market ve Ticaret Sistemi

Oyuncu kendi Marketini inşa eder. Market, Savaş Fonu (altın) kazanmanın birincil yoludur. Market, Ana Depo'dan minimum belli bir mesafe uzakta kurulmak zorundadır. Kamyonlar sabit kur üzerinden kaynağı Markete bırakır. Kamyonlar altını (Savaş Fonu) alıp depoya geri döner. Arayüzden kaynakların yüzde kaçının Markete yönlendirileceği belirlenir. Market kamyonları düşman tarafından hedeflenebilir. Stratejik ikilem şudur: daha fazla Savaş Fonu demek daha az depodaki kaynak demektir; daha az Market ticareti ise teknoloji araştırması yapılamaması anlamına gelir; denge bulmak zorunludur.

### Rota Yönetimi ve Override

Sistem kamyonları otomatik olarak en kısa ve güvenli rotaya yönlendirir. Yol kesilirse (köprü yıkılması, düşman işgali) AI bekler ve kamyon tıkanır. Oyuncu yeni yol inşa edebilir veya Manuel Override yetkisiyle kamyonları acil durum rotalarına yönlendirebilir. Override edilen kamyonlar normal AI yönlendirmesine dönene kadar manuel kontrol altındadır.

### Yol Yükseltme Sistemi

İki yol türü vardır. Toprak Yol, baz hız çarpanı 1.0x'tir, maliyeti düşüktür, başlangıç seviyesidir ve yağmurda yavaşlar. Asfalt Yol, hız çarpanı 1.5x–2.0x'tir, orta maliyettedir (demir gerektirir) ve standart hızlı ulaşım sağlar.

## Savaş, İkmal ve Cephe Hattı

Savaş alanında mühimmatın bitmesi ölümcüldür.

### İkmal Sistemi — Talep-Çekim Mekanizması

İleri Karakollar (Forward Outposts / FOB) ikmal sisteminin merkezidir. Her FOB etrafında bir ikmal alanı (AoE) yaratır. Bu alan gerideki depolardan otomatik olarak mühimmat, yakıt ve erzak çeker. Ana savunma kuleleri (SAM vb.) bu FOB'ların içine kurulmak ve buradan beslenmek zorundadır. FOB yok edilirse, bağlı tüm savunma sistemleri ikmalden kesilir.

### İkmal Akışı

Mühimmat, Silah Fabrikasından Ana Depoya, oradan FOB'a ve nihayetinde piyade ile savunma kulelerine ulaşır. Yakıt (Petrol), Rafineriden Ana Depoya, oradan FOB'a ve nihayetinde araçlar ile hava üssüne ulaşır. Erzak, Gıda Tesisinden Ana Depoya, oradan FOB'a ve nihayetinde piyade mangalarına ulaşır. Nadir Maden, Maden Ocağından Ana Depoya, oradan İleri Teknoloji Tesisine ve nihayetinde radar ile hava kuvvetlerine ulaşır.

### Ceza Sistemi (Debuff)

Lojistik kesintileri ağır bedellere yol açar. Erzak bittiğinde piyade mangalarının savaş gücü ve hızı düşer; kurtarma için ikmal hattını yeniden açmak gerekir. Yakıt bittiğinde araçlar immobilize (hareketsiz) kalır; kurtarma için yakıt ikmal kamyonu göndermek gerekir. Mühimmat bittiğinde tüm savaş birimlerinin ana silahları kullanılamaz hale gelir ve otomatik geri çekilme başlar; kurtarma için FOB'u tekrar beslemek gerekir. İstisna olarak, boşta duran ve hareket etmeyen araçlar (veya pistteki uçaklar) yakıt tüketmez; yakıt sadece aktif hareket sırasında harcanır.

### Taktiksel Çekilme (Mühimmat Bitişi)

Lojistik hat kesilip birliğin mermisi bittiğinde şunlar olur: ana silahlarını kullanamazlar, hızları %110 olur, en yakın güvenli karakola otomatik olarak geri çekilirler. Oyuncu bu sırada geri çekilen birliklere müdahale edebilir ve onları decoy (yem) gibi kullanabilir. Otomatik dönmeyi kapatma seçeneği olmalıdır.

### Birlik Hareketi ve Ölçek — Manga Sistemi

Piyadeler tek tek değil, 5 kişilik mangalar (squad) halinde üretilir. Manga boyutu 5 askerdir. Kışla'dan manga halinde çıkarlar. Zırhlı Personel Taşıyıcı (ZPT) kapasitesi manga sayısına göre belirlenir. Manga içinde bireysel kayıplar yaşanır ve manga tamamen yok olana kadar savaşır.

### Mobilizasyon ve Ulaşım

Birliklerin cepheye intikal yöntemleri şunlardır: Yürüyüş yavaştır, sınırsız kapasitesi vardır ve riski düşüktür (gizli hareket). Askeri Kamyon orta hızdadır, birden fazla manga taşıyabilir ve pusu riski nedeniyle orta risk taşır. Zırhlı Personel Taşıyıcı (ZPT) orta-yüksek hızdadır, kapasitesi manga sayısına bağlıdır ve zırhlı olduğu için riski düşüktür. Helikopter yüksek hızdadır, kapasitesi sınırlıdır ve SAM tehdidi nedeniyle riski yüksektir.

### Arazi Kısıtlamaları

Haritadaki farklı arazi tipleri birliklerin hareketini etkiler. Açık alanda araçlar ve piyadeler serbestçe geçer, standart hareket geçerlidir. Yolda (toprak veya asfalt) araçlar ve piyadeler hızlı geçer, en hızlı rotadır. Temizlenmemiş ormanda araçlar geçemez, piyadeler yavaş geçer (sızma), pusu için idealdir. Dağlık alanda araçlar geçemez, piyadeler çok yavaş geçer ve yüksek savunma avantajı vardır. Nehir ve su geçişlerinde hem araçlar hem piyadeler için köprü gereklidir ve köprü stratejik bir hedeftir.

### Siper ve Savunma Sistemleri

Savunma iki katmandan oluşur. Birinci katman geçici savunmadır: Çukur Siper (Foxhole). Piyadeler anlık çatışmalar için bulundukları yere hızlıca çukur siper kazar. Düşük savunma bonusu sağlar, kalıcı değildir ve acil durumlarda hayatta kalma şansını artırır. İkinci katman kalıcı savunmadır: İstihkâm Sistemleri. İstihkâm birlikleri haritanın geçiş noktalarına kalıcı savunma hatları inşa eder. Siper ağları, dikenli teller ve tank kapanları ile cephe hattı oluşturulur ve bu yapılar stratejik darboğazları kontrol etmek için kullanılır. Savunma yapıları şunlardır: Çukur Siper piyade mangası tarafından inşa edilir, geçicidir ve küçük savunma bonusu sağlar. Siper Ağı istihkâm birliği tarafından inşa edilir, kalıcıdır ve yüksek savunma ile cephe hattı oluşturur. Dikenli Tel istihkâm birliği tarafından inşa edilir, kalıcıdır ve piyade hareketini yavaşlatır. Tank Kapanı istihkâm birliği tarafından inşa edilir, kalıcıdır ve araç geçişini engeller.

### Hava Kuvvetleri

Hava birimleri Airbase (Hava Üssü) mantığıyla çalışır. Uçak veya helikopter pistten kalkar, belirlenen hedefe gider ve görevini icra eder. Mühimmat veya yakıt bittiğinde piste geri döner. Pistte mühimmat ve yakıt alır, yeniden göreve hazırlanır. Pistteyken düşman saldırısına açıktır. Pistte bekleyen uçaklar yakıt tüketmez.

## Harita ve Bilgi Savaşı (Intel Warfare)

Harita kontrolü ve düşmanı kör etme mekanikleri, stratejik derinliğin önemli bir katmanıdır.

### Dinamik Harita ve Tahribat

Harita statik değildir ve oyun sırasında fiziksel olarak değiştirilebilir. Köprü yıkma nehir geçişini engeller, yıkılana kadar kalıcıdır ve Onarım Aracı ile geri alınabilir. Dağ geçidi patlatma geçidi tamamen kapatır, oyun boyunca kalıcıdır ve geri alınamaz. Yol tahrip etme kamyon trafiğini engeller veya yavaşlatır, onarılana kadar sürer ve İnşaat Mangası ile geri alınabilir. Stratejik not: yıkılan köprüleri onarmak için oraya fiziksel olarak bir Onarım Aracı göndermek zorunludur; uzaktan onarım yoktur.

### Radar ve Görüş Sistemi

Savaş sisi (Fog of War) ve istihbarat toplama üç seviyeli radar sistemiyle yönetilir. Seviye 1 Temel Radar savaş sisini kaldırır; birliklerin çıktığı alanlar tekrar sise bürünür. Seviye 2 Sensör Radarı sisli alanlarda hareket tespiti yapar; hareket eden konvoyları haritada ping (sinyal) olarak gösterir (AoE tarzı). Seviye 3 SAM Sistemi hava savunma ve tespit sağlar; hava araçlarını tespit eder ve otomatik füzelerle düşürür.

Görüş mekaniği akışı şöyledir: Hiç radar yoksa tam karanlık (Fog of War) vardır ve çözüm Seviye 1 Radar inşa etmektir. Seviye 1 aktifse radar alanı açıktır ve dışı karanlıktır; çözüm daha fazla radar veya keşif birliği göndermektir. Seviye 2 aktifse sisli alanlarda ping sinyalleri görünür; çözüm sinyallere göre birlik konumlandırmaktır. Seviye 3 aktifse hava tehditleri tespit edilir ve otomatik ateş yapılır; çözüm SAM ağını genişletmektir.

## Üretim ve Asimetrik Factionlar (Ülkeler)

Oyunda genel bir Çağ/Tier atlama sistemi yoktur. Her bina, Savaş Fonu ve az miktarda maden harcayarak kendi içindeki manuel teknoloji kuyruğundan (queue) gelişir. Bu tasarım kararı, Tier sistemi yerine her binanın kendi iç teknoloji ağacına sahip olmasını sağlar ve oyuncunun hangi binayı önceliklendireceğine dair stratejik kararlar vermesini zorunlu kılar.

### Faction Kimlikleri

Dört asimetrik faction bulunur ve her biri farklı bir üretim felsefesi ve lojistik ağırlığıyla öne çıkar.

ABD'nin felsefesi özel sektördür. Üretim hızı çok hızlıdır, birim maliyeti pahalıdır ve temel para birimi Savaş Fonudur (kritik). Uzmanlığı hava indirme birlikleridir ve lojistik yükü Savaş Fonu yönetimidir. Fabrikalar sürekli değil, birim üretirken Savaş Fonu yakar. Güçlü yönü çok hızlı üretim ve pahalı hava indirme birliklerinde ustalıktır. Zayıf yönü Savaş Fonu akışı kesilirse üretimin tamamen durmasıdır. Kritik kaynağı Savaş Fonu (altın) olup, Nadir Maden ihtiyacı yüksek, Erzak ihtiyacı düşük, Petrol ihtiyacı orta ve Demir/Metal ihtiyacı ortadır.

Rusya'nın felsefesi devlet fabrikasıdır. Üretim hızı yavaş ama süreklidir, birim maliyeti ucuzdur ve temel para birimi ham maddedir (Demir/Petrol). Uzmanlığı mekanize birliklerdir ve lojistik yükü petrol lojistiğidir. Savaş Fonu istemez, sadece ham madde kullanır. Güçlü yönü ucuz ve dayanıklı mekanize birlikler ile sürekli üretim kapasitesidir. Zayıf yönü hantal birimler ve sürekli zırhlı üretiminin petrol lojistiğini ağırlaştırmasıdır. Kritik kaynağı Petroldür; Nadir Maden ihtiyacı düşük, Erzak ihtiyacı orta, Savaş Fonu ihtiyacı düşük ve Demir/Metal ihtiyacı yüksektir.

Türkiye'nin felsefesi piyade ve savunmadır. Üretim hızı ortadır, birim maliyeti karmadır (piyade ucuz, teknoloji pahalı) ve temel para birimi karmadır. Uzmanlığı tahkimat ve piyade ordusudur ve lojistik yükü erzak lojistiğidir. Güçlü yönü devasa piyade orduları, güçlü tahkimat sistemleri ve alan kontrolüdür. Zayıf yönü büyük orduların erzak lojistiğini ağırlaştırması ve ileri teknoloji eksikliğidir. Kritik kaynağı Erzaktır; Nadir Maden ihtiyacı düşük, Petrol ihtiyacı orta, Savaş Fonu ihtiyacı orta ve Demir/Metal ihtiyacı yüksektir.

İngiltere'nin felsefesi istihbarattır. Üretim hızı ortadır, birim maliyeti orta-pahalıdır ve temel para birimi Savaş Fonu ile ham maddenin birleşimidir. Uzmanlığı gizli operasyonlardır ve lojistik yükü teknoloji bakımıdır. Radarlara daha zor yakalanan birlikleri ve sistemleri kullanır. Güçlü yönü düşman radarlarından gizlenme yeteneği ve üstün keşif kapasitesidir. Zayıf yönü doğrudan çatışmada sayısal dezavantaj ve yüksek teknoloji maliyetidir. Kritik kaynakları Nadir Madenler ve Savaş Fonudur; Nadir Maden ihtiyacı çok yüksek, Erzak ihtiyacı düşük, Petrol ihtiyacı orta ve Demir/Metal ihtiyacı ortadır.

## Kazanma ve Kaybetme Koşulu

Oyunun tek zafer koşulu Annihilation (Yok Etme) modudur. Düşmanın haritadaki tüm askeri varlıklarının (binalar, birlikler, lojistik unsurlar) tamamen imha edilmesi gerekir. Bu, oyunun lojistik odaklı doğasıyla uyumludur: düşmanın lojistik zincirini kırmak, üretimini durdurmak ve son birliğini de yok etmek gerekir.
