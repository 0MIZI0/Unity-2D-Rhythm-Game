from pathlib import Path


from tkinter import *
from tkinter import ttk
from tkinter import Canvas, Entry, Text, Button, PhotoImage, messagebox

import pymysql
import webbrowser

import glob
import os.path


login = {}

OUTPUT_PATH = Path(__file__).parent
ASSETS_PATH = OUTPUT_PATH / Path("./assets")

def relative_to_assets(path: str) -> Path:
    return ASSETS_PATH / Path(path)

MUSIC_PATH = OUTPUT_PATH / Path("./music")

def gamestart():
    global login
    MEMO_PATH = OUTPUT_PATH / Path("./logininfo.txt")
    File = open(MEMO_PATH, "w")
    File.write(login["username"])
    File.write("\n")
    File.write(login["userprofile"])
    File.write("\n")
    File.write(str(login["score"]))
    File.write("\n")
    File.write(str(login["playtime"]))
    File.close()
    #게임 실행하는 코드 적어놓기
    exit()

conn = pymysql.connect(host="10.122.2.174", user="root", password="root", db="userdb", charset="utf8")
cur = conn.cursor(pymysql.cursors.DictCursor)

sql = "SELECT * from users"
cur.execute(sql)
results = cur.fetchall()

info = {}


for i in results:
    info[i['username']] = i['password']

cur.close()
conn.commit()
conn.close()


def checklogin():
    global login
    tmid = id.get()
    tmps = ps.get()
    if tmid in info:
        if info[tmid] != tmps:
            messagebox.showinfo ('안내 메세지','올바르지 않은 비밀번호입니다',icon = 'error')
        else:
            messagebox.showinfo ('안내 메세지','로그인 완료!')
            window.destroy()

            def musicdown():
                global login
                root.destroy()
                winroot = Tk()
                winroot.title("NonStop!")
                winroot.iconbitmap(relative_to_assets('icon.ico'))
                lnk = StringVar()           
                winroot.geometry("1280x720")
                winroot.configure(bg = "#6F52BC")

                def convert():
                    #유튜브 전용 인스턴스 생성
                    par = lnk.get()
                    print(par)

                    yt = YouTube(par)

                    yt.streams.filter(only_audio=True).all()
                    yt.streams.filter(only_audio=True).first().download()
                    name = yt.title + ".mp4"
                    name2 = yt.title + ".mp3"
                    files = glob.glob('/*.mp4')
                    for x in files:
                        if not os.path.isdir(x):
                            filename = os.path.splitext(x)
                            try:
                                os.rename(x,filename[0] + '.mp3')
                            except:
                                pass
                    MUSIC_PATH_TEMP = str(MUSIC_PATH) + '/'+ name2
                    os.rename(name, MUSIC_PATH_TEMP)
                    messagebox.showinfo("success",yt.title + 'download complete!') #메시지 박스를 띄운다.
                    lnk.set("")

                canvas = Canvas(
                    winroot,
                    bg = "#6F52BC",
                    height = 720,
                    width = 1280,
                    bd = 0,
                    highlightthickness = 0,
                    relief = "ridge"
                )

                canvas.place(x = 0, y = 0)
                image_image_1 = PhotoImage(
                    file=relative_to_assets("image_1.png"))
                image_1 = canvas.create_image(
                    848.0,
                    360.0,
                    image=image_image_1
                )

                image_image_2 = PhotoImage(
                    file=relative_to_assets("image_3.png"))

                image_2 = canvas.create_image(
                    640.0,
                    200.0,
                    image=image_image_2
                )

                button_image_1 = PhotoImage(
                    file=relative_to_assets("button_3.png"))
                button_1 = Button(
                    image=button_image_1,
                    borderwidth=0,
                    highlightthickness=0,
                    command=convert,
                    relief="flat"
                )
                button_1.place(
                    x=461.0,
                    y=400.0,
                    width=357.0,
                    height=70.0
                )                
                entry_1 = Entry(
                    bd=0,
                    bg="#FFFFFF",
                    highlightthickness=0,
                    font = "Consolas 20 bold",
                    textvariable = lnk
                )
                entry_1.place(
                    x=204.0,
                    y=299.0,
                    width=871.0,
                    height=42.0
                )
                entry_3 = Label(
                    text = "insert youbute link",
                    font = "Consolas 15 bold",
                    fg="white",
                    bg="#7052BC",
                    anchor="center"
                )
                button_image_11 = PhotoImage(
                    file=relative_to_assets("button_11.png"))
                button_11 = Button(
                    image=button_image_11,
                    borderwidth=0,
                    highlightthickness=0,
                    command=gamestart,
                    relief="flat"
                )
                button_11.place(
                    x=880.0,
                    y=569.0,
                    width=357.0,
                    height=95.0
                )
                entry_3.place(
                    x=530.0,
                    y=260.0,
                )
                winroot.resizable(False, False)
                winroot.mainloop()

            for i in results:
                if i["username"] == tmid:
                    login = i
                    break
            
            root = Tk()
            root.title("NonStop!")
            root.iconbitmap(relative_to_assets('icon.ico'))
            root.geometry("1280x720")
            root.configure(bg = "#6F52BC")

            canvas = Canvas(
                root,
                bg = "#6F52BC",
                height = 720,
                width = 1280,
                bd = 0,
                highlightthickness = 0,
                relief = "ridge"
            )

            canvas.place(x = 0, y = 0)

            image_image_11 = PhotoImage(
                file=relative_to_assets("image_1.png"))
            image_11 = canvas.create_image(
                848.0,
                360.0,
                image=image_image_11
            )

            image_image_33 = PhotoImage(
                file=relative_to_assets("image_3.png"))
            image_33 = canvas.create_image(
                276.0,
                151.0,
                image=image_image_33
            )

            image_image_22 = PhotoImage(
                file=relative_to_assets("image_2.png"))
            image_22 = canvas.create_image(
                355.0,
                387.0,
                image=image_image_22
            )

            button_image_11 = PhotoImage(
                file=relative_to_assets("button_11.png"))
            button_11 = Button(
                image=button_image_11,
                borderwidth=0,
                highlightthickness=0,
                command=gamestart,
                relief="flat"
            )
            button_11.place(
                x=880.0,
                y=569.0,
                width=357.0,
                height=95.0
            )

            button_image_22 = PhotoImage(
                file=relative_to_assets("button_22.png"))

            button_22 = Button(
                image=button_image_22,
                borderwidth=0,
                highlightthickness=0,
                command=musicdown,
                relief="flat"
            )
            button_22.place(
                x=880.0,
                y=450.0,
                width=357.0,
                height=95.0
            )
            Label_1 = Label(
                text = login["username"],
                font = "Consolas 40 bold",
                fg="white",
                bg="#7052BC"
            )

            Label_1.place(
                x=420.0,
                y=235.0
            )
            if len(login["userprofile"]) > 15:
                Label_2 = Label(
                    text = login["userprofile"][0:15] + "...",
                    font = "Consolas 40 bold",
                    fg="white",
                    bg="#7052BC"
                )
            else:
                Label_2 = Label(
                    text = login["userprofile"],
                    font = "Consolas 40 bold",
                    fg="white",
                    bg="#7052BC"
                )

            Label_2.place(
                x=345.0,
                y=305.0
            )

            Label_3 = Label(
                text = str(login["score"]),
                font = "Consolas 40 bold",
                fg="white",
                bg="#7052BC"
            )

            Label_3.place(
                x=340.0,
                y=380.0
            )

            Label_4 = Label(
                text = str(login["playtime"]),
                font = "Consolas 40 bold",
                fg="white",
                bg="#7052BC"
            )

            Label_4.place(
                x=390.0,
                y=450.0
            )
            
            root.resizable(False, False)
            root.mainloop()
           

    else:
            messagebox.showinfo ('안내 메세지','존재하지 않는 아이디입니다',icon = 'error')

def register():
    webbrowser.open("http://seeshape.kr/")


window = Tk()

window.title("NonStop!")
window.iconbitmap(relative_to_assets('icon.ico'))

id, ps = StringVar(), StringVar()

window.geometry("1280x720")
window.configure(bg = "#6F52BC")


canvas = Canvas(
    window,
    bg = "#6F52BC",
    height = 720,
    width = 1280,
    bd = 0,
    highlightthickness = 0,
    relief = "ridge"
)

canvas.place(x = 0, y = 0)
image_image_1 = PhotoImage(
    file=relative_to_assets("image_1.png"))

image_1 = canvas.create_image(
    848.0,
    360.0,
    image=image_image_1
)

image_image_2 = PhotoImage(
    file=relative_to_assets("id.png"))

image_2 = canvas.create_image(
    490.0,
    360.0,
    image=image_image_2
)

image_image_3 = PhotoImage(
    file=relative_to_assets("image_3.png")
)

image_3 = canvas.create_image(
    640.0,
    240.0,
    image=image_image_3
)

image_image_4 = PhotoImage(
    file=relative_to_assets("pw.png"))

image_4 = canvas.create_image(
    470.0,
    420.0,
    image=image_image_4
)

entry_1 = Entry(
    bd=0,
    bg="#FFFFFF",
    highlightthickness=0,
    font = "Consolas 20 bold",
    textvariable = id
)

entry_1.place(
    x=500.0,
    y=330.0,
    width=330.0,
    height=43.0
)

entry_2 = Entry(
    bd=0,
    bg="#FFFFFF",
    highlightthickness=0,
    font = "Consolas 20 bold",
    textvariable = ps
)

entry_2.place(
    x=500.0,
    y=395.0,
    width=330.0,
    height=43.0
)

button_image_1 = PhotoImage(
    file=relative_to_assets("button_1.png"))

button_1 = Button(
    image=button_image_1,
    borderwidth=0,
    highlightthickness=0,
    relief="flat",
    command = checklogin,
    text = "Login"
)

button_1.place(
    x=490.0,
    y=483.0,
    width=300.0,
    height=45.0
)

button_image_2 = PhotoImage(
    file=relative_to_assets("button_2.png")
)

button_2 = Button(
    image=button_image_2,
    borderwidth=0,
    highlightthickness=0,
    relief="flat",
    command = register,
    text = "Login"
)

button_2.place(
    x=490.0,
    y=548.0,
    width=300.0,
    height=45.0
)

window.resizable(False, False)
window.mainloop()
