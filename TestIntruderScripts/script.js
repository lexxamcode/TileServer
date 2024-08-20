function downloadURI(uri, name) {
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    delete link;
}

function bulkDownload(baseUri, zoom) {
    for (let x = 0; x < Math.pow(4, zoom); x++) {
        for (let y = 0; y < Math.pow(4, zoom); y++) {
            let downloadUri = `${baseUri}/${zoom}/${x}/${y}.png`
            downloadURI(downloadUri, `Tiles/${zoom}/${x}/${y}.png`)
        }
    }
}

function downloadClicked() {
    bulkDownload("https://tile.openstreetmap.org", 12);
}