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
        bg: '#ECECE6',
        panel: '#F4F4EF',
        card: '#FBFBF7',
        'card-hover': '#F0F0EA',
        line: 'rgba(20, 22, 25, 0.12)',
        'line-strong': 'rgba(20, 22, 25, 0.22)',
        text: '#17181B',
        muted: '#5F6166',
        'muted-strong': '#45474C', // darker body text (e.g. tagline)
        dim: '#8A8C92',
        accent: '#1487CE',
        'accent-strong': '#0E6FB0',
        // surface/effect tokens
        topbar: 'rgba(226, 226, 219, 0.85)', // frosted top bar (slightly darker than ground)
        ring: 'rgba(20, 135, 206, 0.22)',    // accent glow / focus ring
        wash: 'rgba(20, 135, 206, 0.05)',    // ambient background tint
        shadow: 'rgba(20, 22, 25, 0.15)',    // card elevation
      },
    },
  },
  plugins: [],
}
