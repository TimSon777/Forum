const icons = ['islands#redIcon', 'islands#greenIcon', 'islands#orangeIcon', 'islands#blueIcon'];

function getRandomIcon() {
    const i = Math.floor(Math.random() * icons.length);
    return icons[i];
}

function getCenter(office) {
    const centerCoordinates = office.coordinate;
    return [centerCoordinates.latitude, centerCoordinates.longitude];
}

function addressToStr(office) {
    const address = office.address;
    return [address.city, address.street, address.buildingNumber].join(' ')
}

function convertOffices(offices) {
    return offices.map((e) => {
        return {
            name: e.name,
            style: getRandomIcon(),
            center: getCenter(e),
        }
    })
}

function fillMenu(office, menu, map) {
    const item = $('<li><a href="#">' + office.name + '</a></li>');
    const placeMark = new ymaps.Placemark(office.center, {balloonContent: office.name});
    const collection = new ymaps.GeoObjectCollection(null, {preset: office.style});
    map.geoObjects.add(collection);
    collection.add(placeMark);
    item
        .appendTo(menu)
        .find('a')
        .bind('click', () => {
            if (!placeMark.balloon.isOpen()) {
                placeMark.balloon.open();
            } else {
                placeMark.balloon.close();
            }
            return false;
        });
}

function createMap(center) {
    return new ymaps.Map('map', {
        center: center,
        zoom: 10
    }, {
        searchControlProvider: 'yandex#search'
    });
}

ymaps.ready(init);

function init() {
    $.get({
        url: '/Company/Offices',
        success: (array) => {
            const center = getCenter(array[0]);

            const map = createMap(center);

            const menu = $('<ul class="menu"></ul>');

            const offices = convertOffices(array);

            for (let i = 0; i < offices.length; i++) {
                fillMenu(offices[i], menu, map);
            }

            menu.appendTo($('#map-menu'));
            map.setBounds(map.geoObjects.getBounds());
        }
    });
}