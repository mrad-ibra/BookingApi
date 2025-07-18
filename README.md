# BookingApi

# AvailableHomesController.cs
// Controller yalnız HTTP səviyyəsində sorğu qarşılayır və yönləndirmə işini görür.
// Burada əsas diqqət validation və error handling-dir. Əsas biznes məntiq servisdədir.
// Error cavabları istifadəçiyə detallı və JSON formatında qaytarılır ki, frontend rahat emal etsin.
// CancellationToken vasitəsilə uzun sorğular ləğv edilə bilər.
// Swagger annotasiyaları ilə endpoint sənədləşməsi avtomatik olur.

# HomeAvailabilityService.cs
// Bütün əsas filter məntiqi bu servisdə yerləşdirilib.
// Tarixlər üzərində HashSet istifadə edərək sürətli .IsSubsetOf() ilə yoxlama aparılır.
// AsParallel() ilə çox nüvəli emal dəstəklənir ki, performans aşağı düşməsin.
// Stopwatch istifadə olunur ki, istəyə cavab vermə müddəti loglansın.
// Kod daha sonra DB və ya Redis kimi servislərə keçid üçün hazırdır, çünki interfeys üzərindən işləyir.


# InMemoryHomeData.cs

 // Demo üçün slot məlumatları statik olaraq yüklənir.
// Real tətbiqdə bu hissə Redis, DB və ya API source ilə əvəz oluna bilər.
// Thread-safety üçün ConcurrentDictionary istifadə olunub.


# AvailableHomeResponse.cs

// Response modeli API cavabının frontend üçün stabil və asan istifadəyə yararlı olması üçün dizayn olunub.
// Domain modeli ilə qarışmaması üçün ayrıca DTO struktur yaradılıb.
