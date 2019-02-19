// gebruik de serialport package
const SerialPort = require('serialport');
const dgram = require('dgram');
const ip = '192.168.1.249'; // IP met .255 op einde voor multicast address.
const port = 8008;
const udpServer = dgram.createSocket('udp4');

//Test
//udpServer.send('test', port, ip);

let serialport = new SerialPort("COM5", { baudRate : 57600}, (err) => {
    if(err)
    {
        console.error("Error setting up serialport", err.message);
        process.exit();
    }
});

serialport.on('open', () => {
    console.log("opened serial");
    //setTimeout(function(){ serialport.write("5050505050505050;"); }, 500);  
});

let incomingString = "";

serialport.on('data', (data) => {
    data.forEach(byte => {
        switch(byte)
        {
            case 0x0d: // CR: Carriage return
            case 0x0a: // LF: Line Feed / Newline (volgens mij kan die regel hierboven weggelaten worden, kan echter andersom zijn)
                if(incomingString != "")
                {
                    //console.log(incomingString);
                    udpServer.send(incomingString,port,ip);
                    incomingString = "";
                }
                break;
            default:
                let char = String.fromCharCode(byte);
                incomingString += char;
                break;
        }
    });
});

const socket = dgram.createSocket('udp4');
const portrx = 8006;
//const ip = "192.168.2.182"; // luister op alle IP adressen van de machine met 0.0.0.0

socket.on('message', (data) => {
    console.log(data.toString());
    let string = data.toString();
    serialport.write(string);
})

socket.bind(portrx,ip);