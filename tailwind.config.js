/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Views/**/*.cshtml"],
  theme: {
    colors: {
      'royalBlue': '#003366',
      'cream': '#FFFAF0',
      'darkBlue': '#001B36',
    },
    fontFamily: {
      'merriweather': ['Merriweather', 'serif'], 
      'notosans': ['Noto Sans', 'sans-serif'],
    },
    extend: {},
  },
  plugins: [],
}