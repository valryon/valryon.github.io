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
    extend: {
      colors: {
        bg: '#0E1420',
        panel: '#141B2A',
        card: '#172032',
        'card-hover': '#1E2A46',
        line: 'rgba(148, 163, 184, 0.13)',
        'line-strong': 'rgba(148, 163, 184, 0.24)',
        text: '#F1F5FB',
        muted: '#93A2B8',
        dim: '#64748B',
        accent: '#818CF8',
        'accent-strong': '#6366F1',
      },
    },
  },
  plugins: [],
}
