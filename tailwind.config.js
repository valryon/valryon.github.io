/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './_layouts/**/*.html',
    './_includes/**/*.html',
    './_posts/**/*.{md,html}',
    './_projects/**/*.{md,html}',
    './_protos/**/*.{md,html}',
    './*.html',
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
