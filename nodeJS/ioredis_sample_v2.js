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
],
{
  slotsRefreshInterval: 5000  // 이 옵션이 꼭 필요함.
}

);

// cluster.set("node", "js");
// cluster.get("node", (err, res) => {
//   // res === 'bar'
//   console.log("Well Read")
// });


insert = function(a, callback){
  let today = new Date();
  let m = today.getMinutes();
  let s = today.getSeconds();

  // 키 값 설정
  var key = "node" + a;
  var val = m + ":" + s + " -- " + "node" + a;

  // 데이터 입력
  cluster.set(key, val);

  // 데이터 출력
  cluster.get(key, (err, reply) => {
    if (err) throw err;
    console.log(reply)
});
}

const sleep = (ms) => {
  return new Promise(resolve=>{
      setTimeout(resolve,ms)
  })
}

const init = async () => {
  for (var i = 0; i < 301; i++){
    insert(i);
    await sleep(3000);
  }
}

init();



