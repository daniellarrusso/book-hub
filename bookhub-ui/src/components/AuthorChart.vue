<template>

    <Bar :data="chartData" :options="chartOptions" />

</template>

<script setup lang="ts">
import { Bar } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

// Mock data: avg rating per author
const authors = [
  { author: "J.R.R. Tolkien", yourAvg: 4, avgRating: 4.6 },
  { author: "George Orwell", yourAvg: 5, avgRating: 4.1 },
  { author: "Frank Herbert", yourAvg: 3, avgRating: 4.2 },
  { author: "Jane Austen", yourAvg: 2, avgRating: 3.9 }
]

const chartData = {
  labels: authors.map(a => a.author),
  datasets: [
    {
      label: "Your Avg Rating",
      data: authors.map(a => a.yourAvg),
      backgroundColor: '#36A2EB'
    },
    {
      label: "Average Rating (Others)",
      data: authors.map(a => a.avgRating),
      backgroundColor: '#FF6384'
    }
  ]
}

const chartOptions = {
  responsive: true,
  plugins: {
    title: {
      display: true,
      text: "Your Average Rating vs. Others (per Author)"
    }
  },
  scales: {
    y: {
      min: 0,
      max: 5,
      ticks: { stepSize: 1 }
    }
  }
}
</script>
