using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager//bu tüm cache alternatifleri için kullanılacak interface.//Alternatife göre aspect ve injectionları yazıp direkt olarak cache değiştirebiliriz.
    {
        T Get<T>(string key);//data tutabiliriz.farklı farklı veritipi dönebileceğimiz için generic method yazıyoruz.
                             //cacheden gelirken hangi tipte istediğimizi,hangi tiple çalıştığımızı ve hangi tipe dönüştürülmesi gerektiğini
                             //söylüyo olcaz.Ben sana bir key vereyim sen bellekte o keye karşılık gelen datayı bana ver.
        object Get(string key);//farklı alternatif-> typecasting gereklidir.
        void Add(string key, object value,int duration);//cache'e ekleme yapmak(key,gelecek data bir obje olacak obje herşeyi tutabilir,ne kadar duracağı dakika saat vs.)
        bool IsAdd(string key);//Eklerken cache'de var mı diye bir kontrol yapmalıyız.
        void Remove(string key);//cacheden uçurma
        void RemoveByPattern(string pattern);//parametreli olanları uçurmak için pattern mesela ismi getle başlayan yada isminde caregory olanları uçur gibi.
    }
}
