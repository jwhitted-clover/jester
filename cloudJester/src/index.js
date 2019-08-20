const grpc = require('grpc');
const protoLoader = require('@grpc/proto-loader');

const service = require('./service');

const PROTO_PATH = __dirname + '../../proto/SdkDriver.proto';

const packageDefinition = protoLoader.loadSync(PROTO_PATH, {
  keepCase: true,
  longs: String,
  enums: String,
  defaults: true,
  oneofs: true,
});
const proto = grpc.loadPackageDefinition(packageDefinition).Jester;

const GRPC_HOST = '0.0.0.0';
const GRPC_PORT = 12346;

const main = () => {
  const server = new grpc.Server();
  server.addService(proto.SdkDriver.service, service);
  server.bind(
    `${GRPC_HOST}:${GRPC_PORT}`,
    grpc.ServerCredentials.createInsecure()
  );
  server.start();
};
main();
