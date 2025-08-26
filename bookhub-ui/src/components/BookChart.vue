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

// Mock data: each book has your rating vs avg rating
const books = [
  { title: "The Hobbit", yourRating: 4, avgRating: 4.5 },
  { title: "1984", yourRating: 5, avgRating: 4.2 },
  { title: "Dune", yourRating: 3, avgRating: 4.0 },
  { title: "Pride and Prejudice", yourRating: 2, avgRating: 3.8 }
]

const chartData = {
  labels: books.map(b => b.title),
  datasets: [
    {
      label: "Your Rating",
      data: books.map(b => b.yourRating),
      backgroundColor: '#4BC0C0'
    },
    {
      label: "Average Rating (Others)",
      data: books.map(b => b.avgRating),
      backgroundColor: '#FFCE56'
    }
  ]
}

const chartOptions = {
  responsive: true,
  plugins: {
    title: {
      display: true,
      text: "Your Ratings vs. Average Ratings (per Book)"
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
