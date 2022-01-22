function Slugify(text) {
    var trMapping = {
        'çÇ': 'c',
        'ğĞ': 'g',
        'şŞ': 's',
        'üÜ':'u',
        'öÖ':'o',
        'ıİ':'i' 
    };

    for (var key in trMapping) {
        text = text.replace(new RegExp('[' + key + ']', 'g'), trMapping[key]); 
    }

    return text.replace(/[^-a-zA-Z0-9\s]+/ig, '')//alfanumerik olmayan karakterleri sil
        .replace(/\s/gi, "-")//boşlukları - ile değiştir
        .toLowerCase()
        .substring(0, 75);

}