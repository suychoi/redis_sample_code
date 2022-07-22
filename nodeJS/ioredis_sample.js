const Redis = require("ioredis");

// node ./r_conn.js 실행

const cluster = new Redis.Cluster([
  {
    port: 1521,
    host: "172.30.5.167",
    password: "foobared"
  },
  {
    port: 3950,
    host: "172.30.5.167",
    password: "foobared"

  },
  {
    port: 6379,
    host: "172.30.5.167",
    password: "foobared"

  },
  {
    port: 1521,
    host: "172.30.5.186",
    password: "foobared"

  },
  {
    port: 3950,
    host: "172.30.5.186",
    password: "foobared"

  },
  {
    port: 6379,
    host: "172.30.5.186",
    password: "foobared"
  }
]);

// cluster.set("node", "js");
// cluster.get("node", (err, res) => {
//   // res === 'bar'
//   console.log("Well Read")
// });



for (var i = 0; i < 101; i++){

    let today = new Date();
    let s = today.getSeconds();
    let ms = today.getMilliseconds();

    var key = "node" + i;
    var val = s + ":" + ms;
    cluster.set(key, val);
    cluster.get(key, (err, reply) => {
        if (err) throw err;
        console.log(reply)    
    });
    await sleep(1000);
}
