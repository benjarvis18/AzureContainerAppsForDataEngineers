<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <chart v-if="loaded" :chart-data="chartData" :options="options" />
      </v-col>
    </v-row>
    <v-row class="text-center">
      <v-col cols="12">
        <v-data-table
          :headers="headers"
          :items="alerts"
          :items-per-page="10"
          :sort-by="['dateTime']"
          :sort-desc="[true]"
          class="elevation-1"
        ></v-data-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import axios from "axios";
import Chart from "./Chart.vue";

export default {
  name: "HelloWorld",
  components: { Chart },

  data() {
    return {
      summary: null,
      loaded: false,
      headers: [
        {
          text: "Alert Date",
          value: "dateTime",
        },
        {
          text: "Sensor Name",
          value: "sensorName",
        },
        { text: "Severity", value: "severity" },
        { text: "Message", value: "message" },
      ],
      options: {
        responsive: true,
        maintainAspectRatio: false,
      },
    };
  },

  computed: {
    alerts() {
      if (!this.summary || this.summary.length == 0) {
        return [];
      }

      return this.summary
        .map((s) => {
          return s.data.map((d) => {
            return d.alerts.map((a) => {
              return {
                dateTime: d.dateTime,
                sensorName: s.sensorName,
                severity: a.severity,
                message: a.message,
              };
            });
          });
        })
        .flat(2);
    },
    chartData() {
      return {
        labels: this.summary[0].data.map((d) => {
          return d.dateTime;
        }),
        datasets: [
          {
            label: "Sensor 1",
            data: this.summary[0].data.map((d) => d.temperature.average),
            backgroundColor: "transparent",
            borderColor: "rgba(1, 116, 188, 0.50)",
            pointBackgroundColor: "rgba(171, 71, 188, 1)",
          },
        ],
      };
    },
  },

  methods: {
    pollData() {
      setInterval(() => {
        axios.get("/api/sensor-summary").then((response) => {
          this.summary = response.data;

          if (this.summary.length > 0) {
            this.loaded = true;
          }
        });
      }, 3000);
    },
  },
  created() {
    this.pollData();
  },
};
</script>
