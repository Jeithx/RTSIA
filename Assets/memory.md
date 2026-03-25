# Iron Arteries — Memory (Proje Belleği)

## Proje Kimliği
- **İsim:** Project: Iron Arteries (Demir Damarlar)
- **Tür:** ECS/DOTS tabanlı lojistik odaklı Gerçek Zamanlı Strateji (RTS)
- **Engine:** Unity (DOTS — Entities, Burst, Jobs)
- **Assembly:** `IronArteries.asmdef` — `Assets/Scripts/` altında
- **GDD:** `Assets/Iron_Arteries_GDD_V3_Plaintext.md`

---

## Mimari Kararlar

| Karar | Neden |
|---|---|
| Saf ECS (ISystem + IJobEntity) | Binlerce entity performansı için Burst-Compiler uyumluluğu zorunlu |
| NavMesh sadece kara birimleri için | Uçaklar `AirMovementSystem` ile dümdüz uçar, NavMesh kullanmaz |
| `DisableRendering` ile Fog of War | Gerçek GPU culling; işçi-CPU arası senkronizasyon gerek kalmaz |
| Managed `SystemBase` sadece NavMesh ve MarketUI için | `UnityEngine.AI.NavMesh.CalculatePath` Burst-unsafe; diğer her şey `ISystem` |
| `FactionType.USA` = Oyuncu | FogOfWar ve GameMode sistemi şimdilik sabit USA=oyuncu varsayır |
| `TerrainGridComponent` singleton | NativeArray bazlı O(1) terrain hız çarpanı; runtime değiştirilebilir |
| Kamyon yoksa Market soyut | Fiziksel kamyon Entity'si henüz yok; Market ECS değişimi anlık yapıyor |

---

## Tamamlanmış Sistemler (Kod Seviyesi)

### Muharebe
- `CombatResolveSystem` — Otomatik hedefleme, hasar, ölüm
- `FactionRuleSystem` — Asimetrik ülke bonusları
- `SquadSystem` — HP → manga boyutu ölçekleme
- `FoxholeSystem` — 3s hareketsizlik → CoverBonus (%50 hasar azalt)
- `TacticalWithdrawalTag` — Mühimmat bitti → otomatik geri çekil

### Hareket & Pathfinding
- `NavMeshPathfindingSystem` — `UnityEngine.AI.NavMesh.CalculatePath` stabil API
- `UnitMovementSystem` — Waypoint takibi + TerrainGrid hız çarpanı
- `AirMovementSystem` — NavMesh'siz durum makinesi (6 faz)
- `AirbaseSystem` — Dönüş koordinatı güncelleme + mühimmat yenileme

### Lojistik & Ekonomi
- `AoEDepotSystem`, `FactoryProductionSystem`, `ResourceGatheringSystem`
- `FOBSupplySystem` — FOB AoE ikmal
- `LogisticsCargoSystem` / `MarketTradingSystem` — Soyut ticaret

### İnşaat & Üretim
- `BuildingAuthoring` + Baker — Depot, Factory, Market, FOB, Barracks, RadarTower
- `BuildingPlacementSystem` + `BuildingPlacementUI` — B tuşu ghost preview, sol tık onayla
- `UnitProductionSystem` — Kuyruk, kaynak kesimi, kapıda spawn

### Görüş & İstihbarat
- `FogOfWarSystem` — Enemy `DisableRendering` toggle (F tuşu debug)
- `RadarSystem` — L1 görüş, L2 ping blip (3s görünür), L3 SAM anlık imha
- `RadarPingDecaySystem` — Ping ömrü bitti → tekrar gizle

### Oyun Durumu
- `GameModeSystem` — Annihilation (düşman sıfır → zafer)
- `GameStateComponent` — WarmingUp / InProgress / Victory / Defeat
- `PlayerInputSystem` — Tek tık seç, sürükle-bırak çoklu seç, F tuşu FoW, sağ tık hareket

---

## Önemli Dosya Yolları

| Dosya | Açıklama |
|---|---|
| `Assets/Scripts/IronArteries.asmdef` | Ana assembly tanımı |
| `Assets/Scripts/Components/UnitComponents.cs` | Tüm birim komponentleri |
| `Assets/Scripts/Components/MapComponents.cs` | Terrain, FoW tile komponentleri |
| `Assets/Scripts/Components/ResourceComponents.cs` | StockpileComponent, DepotComponent, FactoryComponent |
| `Assets/Scripts/Components/LogisticsComponents.cs` | FOB, Market komponentleri |
| `Assets/Scripts/Systems/PlayerInputSystem.cs` | Tüm oyuncu girdisi |
| `Assets/Scripts/Systems/FogOfWarSystem.cs` | L1 FoW ana sistemi |
| `Assets/Scripts/Systems/RadarSystem.cs` | L2+L3 radar mantığı |
| `Assets/Scripts/Components/Authoring/BuildingAuthoring.cs` | Tüm bina türleri Baker |
| `Assets/Scripts/Components/Authoring/UnitAuthoring.cs` | Tüm birim türleri Baker |

---

## Kritik Eksikler (Sıradaki Hedefler)

| # | Sistem | GDD Referansı |
|---|---|---|
| 1 | **İnşaat Mangası** — fiziksel olarak inşaat noktasına giden birim | GDD §2 |
| 2 | **Fiziksel Kamyon** — haritada görünür, pusuya açık lojistik araç | GDD §3 |
| 3 | **Onarım Aracı** — köprü/yol onarımı için fiziksel araç | GDD §9 |
| 4 | **İstihkâm Birliği** — Siper ağı/Dikenli tel/Tank kapanı inşaatçısı | GDD §9 |
| 5 | **Faction Üretim Asimetrisi** — Rusya/ABD/İngiltere tam kuralları | GDD §14 |
| 6 | **Yol Yükseltme** — Toprak → Asfalt (1.5x hız bonusu) | GDD §4 |
| 7 | **ZPT Taşıma Kapasitesi** — Manga yükleme/boşaltma sistemi | GDD §8 |
| 8 | **Manuel Rota Override** — Kamyon acil dur yönlendirme | GDD §4 |
| 9 | **Bina Teknoloji Kuyruğu** — Bina başına upgrade kuyruğu | GDD §13 |

---

## Bilinen Teknik Notlar

- `[BurstCompile]` sadece unmanaged `ISystem`'a uygulanır; `SystemBase`'e uygulanmamalı
- `MarketUISystem` şimdilik stub — UI Toolkit entegrasyonu bekliyor
- `FactionType.USA` oyuncu olarak hardcode; multiplayer için değiştirilmeli
- `GatherFriendlySightJob` stub olarak bırakıldı; sync loop tercih edildi (performans yeterli)
- Radar L3 SAM anlık hit (delay yok); ileride projectile Entity eklenebilir
- `TerrainGridComponent` runtime oluşturulmalı (Bootstrap veya MapAuthoring ile)
