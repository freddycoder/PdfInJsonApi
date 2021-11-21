import { check } from 'k6';
import http from 'k6/http';

export let options = {
  stages: [
    { target: 2, duration: '2s' },
    { target: 2, duration: '300s' },
    { target: 0, duration: '2s' }
  ],
};

var url = "http://localhost:5280/Pdf";

export function setup () {
    return http.get(url).json();
}

export default function (data) {
    for (var i = 0; i < data.length; i++) {
        var res = http.get(url + "/" + data[i].id, {
            headers: {
              "Accept-Encoding": "gzip"
            }
        });

        check(res, {
            "status is 200": (r) => r.status == 200,
        });
    }
}