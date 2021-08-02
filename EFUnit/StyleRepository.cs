using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFUnit
{
    public class StyleRepository
    {
        public void AddStyle(Style[] styles)
        {

            using (var db = new AppContext())
            {
                var styleAdd = styles.Where(p => p.Id == GetStyleId(p)).ToArray();
               
                db.Styles.AddRange(styleAdd);
                db.SaveChanges();
                

            }
        }

        public void RemoveUser(Style[] styles)
        {
            using (var db = new AppContext())
            {
                var styleRemove = styles.Select(u => new Style { Id = GetStyleId(u), Name = u.Name}).Where(u => u.Id > 0).ToArray();
                if (styleRemove.Length > 0)
                {
                    db.Styles.RemoveRange(styleRemove);
                    db.SaveChanges();
                }

            }
        }

        public int GetStyleId(Style Data)
        {
            int ret = 0;

            using (var db = new AppContext())
            {
                
                var find = db.Styles.FirstOrDefault(p => p.Name == Data.Name);

                if (find != null)
                    ret = find.Id;

            }


            return ret;
        }

        public IEnumerable<Style> FindById(int id)
        {
            using (var db = new AppContext())
            {
                var find = db.Styles.Where(p => p.Id == id).ToList();
                return find;
            }
        }

        public IEnumerable<Style> SelectAll()
        {
            using (var db = new AppContext())
            {
                var AllSelect = db.Styles.Select(p => p).ToList();
                return AllSelect;
            }
        }
    }
}
