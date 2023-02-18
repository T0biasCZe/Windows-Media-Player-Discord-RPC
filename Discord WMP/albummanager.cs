using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Discord_WMP {
    public class albummanager {
        public List<pair> pairList = new List<pair>();
        static int attempts = 0;
        public void LoadListFromCsv() {
            using(var reader = new StreamReader("albumsarts.csv"))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                csv.Read();
                csv.ReadHeader();
                pairList = csv.GetRecords<pair>().ToList();
                try {
                    pair per = pairList[0];
                    //MessageBox.Show(per.album);
                }
                catch{
                    attempts++;
                    if(attempts > 10) {
                        MessageBox.Show("Failed to load album art list");
                        return;
                    }
                    else LoadListFromCsv();
                }
            }
        }
        public void writecsv() {
            using(var writer = new StreamWriter("albumsarts.csv"))
            using(var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
                csv.WriteHeader<pair>();
                csv.NextRecord();
                csv.WriteRecords(pairList);
            }
        }

        public string getalbumart(string album, string title) {
            for(int i = 0; i < 10; i++) {
                foreach(pair per in pairList) {
                    if(per.priority == i) {
                        if(per.type == pairtype.albumstring) {
                            if(per.album == album) {
                                return per.filename;
                            }
                        }
                        else if(per.type == pairtype.albumcontains) {
                            if(album.Contains(per.contains)) {
                                if(per.doesntcontain.Length > 1) if(!album.Contains(per.doesntcontain)) continue;
                                return per.filename;
                            }
                        }
                        else if(per.type == pairtype.titlecontains) {
                            if(title.Contains(per.contains)) {
                                if(per.doesntcontain.Length > 1) if(!title.Contains(per.doesntcontain)) continue;
                                return per.filename;
                            }
                        }
                    }
                }
            }
            return "wmp_empty";
        }
    }
}
