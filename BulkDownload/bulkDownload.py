import urllib.request
import ssl
import os

maxZoom = 6
tileServerUrl = "https://localhost:7147/tiles"
downloadFolder = "DownloadedTiles"
context = ssl._create_unverified_context()


def basicBulkDownload():
    for x in range (0, 2**maxZoom - 1):
        for y in range (0, 2**maxZoom - 1):
            img = urllib.request.urlopen("{tileServerUrl}/{z}/{x}/{y}".format(tileServerUrl=tileServerUrl, z=maxZoom, x=x, y=y), context=context)
            print(maxZoom, x, y)
            input()
            try:
                os.makedirs('./{downloadFolder}/{z}/{x}'.format(downloadFolder=downloadFolder, z=maxZoom, x=x)) 
            except:
                pass
            
            img_file = open('./{downloadFolder}/{z}/{x}/{y}.png'.format(downloadFolder=downloadFolder, z=maxZoom, x=x, y=y), 'wb')
            img_file.write(img.read())
            img_file.close()

def basicBulkDownloadReverse():
    for x in range (2**maxZoom - 1, 0, -1):
        for y in range (2**maxZoom - 1, 0, -1):
            print(maxZoom, x, y)
            input()
            img = urllib.request.urlopen("{tileServerUrl}/{z}/{x}/{y}".format(tileServerUrl=tileServerUrl, z=maxZoom, x=x, y=y), context=context)
            try:
                os.makedirs('./{downloadFolder}/{z}/{x}'.format(downloadFolder=downloadFolder, z=maxZoom, x=x)) 
            except:
                pass
            img_file = open('./{downloadFolder}/{z}/{x}/{y}.png'.format(downloadFolder=downloadFolder, z=maxZoom, x=x, y=y), 'wb')
            img_file.write(img.read())
            img_file.close()

if __name__ == '__main__':
    basicBulkDownloadReverse()