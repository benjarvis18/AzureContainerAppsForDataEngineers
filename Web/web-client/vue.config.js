module.exports = {
  transpileDependencies: ["vuetify"],
  devServer: {
    proxy: "https://integration-api-01.icyriver-ae4d79e3.westeurope.azurecontainerapps.io/",
  },
};
