# Discord rich presence for windows media player
# Features:
* take info from running WMP instance and put it into discord
* minimize onto tray (press minimize button on top right)
* smart album art manager
![obrazek](https://user-images.githubusercontent.com/44525446/219904733-222e0ec0-655f-4a37-adde-f14b7a2f8c6c.png)

# Album art manager

* you must create application on https://discord.com/developers/applications to be able to use custom album art
* create app there, and put the app id into this program
* upload your cover arts + bundled textures into Rich Presence -> art assets
* open art manager by pressing the 3 dots button in this program
* on left, you have list of already paired album arts
* on right, you have 3 options how "pair" album arts with songs
1) by exact album name, past the album name into the correct field, and put the album "key"/filename into the filename key
2) by album name containing specific string. enter string of words into the field, and album art filename that will show for songs that have album name with containing this string. you can also specify that it must not contain some string
3) same as 2 except with song name instead of album name
*you may also enter priority in which the album art is searched. 0 is highest priority, 9 is lower priority
*click close on the album manager window, and it will automatically save (the pairs are saved in albumarts.csv, do not delete the file!)
![obrazek](https://user-images.githubusercontent.com/44525446/219904571-69262432-adab-40d5-aa97-b849181924e0.png)

if you do not want to use the custom album arts, you can use app id "1076519967631093891"
