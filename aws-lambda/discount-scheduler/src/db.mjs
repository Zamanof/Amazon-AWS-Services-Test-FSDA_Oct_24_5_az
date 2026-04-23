import {loadConfig} from "./config.mjs";
import sql from "mssql"
import {connect} from "mssql/lib/global-connection.js";

let poolPromise = null
const config = loadConfig();

function createPoolConfig(){
    return{
        server: config.dbHost,
        port: config.dbPort,
        user: config.dbUser,
        password: config.dbPassword,
        database: config.dbName,
        options:{
            encrypt: config.dbEncrypt,
            trustServerCertificate: config.dbTrustServerCertificate
        },
        pool:{
            max:3,
            min:0,
            idleTimeoutMillis: 30000,
        }
    }
}

export async function getPool(){
    if(!poolPromise){
        poolPromise = new sql.ConnectionPool(createPoolConfig())
            .connect()
            .then((pool)=>{
                console.info("Connected to the pool");
                return pool;
            })
            .catch((err)=>{
                console.error("Failed to connect to the pool");
                poolPromise = null;
                throw err;
            })
    }
    return poolPromise;
}

export async function closePool(){
    if(!poolPromise){
        return;
    }
    const pool = await poolPromise;
    await pool.close();
    poolPromise = null;
}

export {sql};