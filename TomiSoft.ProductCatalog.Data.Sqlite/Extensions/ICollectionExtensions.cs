using System;
using System.Collections.Generic;
using System.Linq;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Extensions {
    public static class ICollectionExtensions {
        public static void RemoveAll<T>(this ICollection<T> collection, Func<T, bool> predicate) {
            if (collection is List<T> list) {
                list.RemoveAll(new Predicate<T>(predicate));
            }
            else {
                List<T> itemsToDelete = collection
                    .Where(predicate)
                    .ToList();

                foreach (var item in itemsToDelete) {
                    collection.Remove(item);
                }
            }
        }
    }
}
