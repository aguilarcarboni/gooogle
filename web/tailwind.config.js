module.exports = {
  darkMode: ["class"],
  content: [
    './pages/**/*.{ts,tsx}',
    './components/**/*.{ts,tsx}',
    './app/**/*.{ts,tsx}',
    './src/**/*.{ts,tsx}',
    './**/*.razor'
	],
  theme: {
    container: {
      center: true,
      padding: "2rem",
      screens: {
      },
    },
    extend: {
      rotate: {
        '30': '30deg',
        '60': '60deg',
        '90': '90deg',
        '120': '120deg'
      },
      colors: {
        background: "hsl(var(--background))",
        foreground: "hsl(var(--foreground))",
        primary: {
          DEFAULT:'#3978D6',
          dark: '#81B7D2'
        },
        secondary: {
          DEFAULT: '#DE7A44',
          dark:'#C35243'
        },
        muted: {
          DEFAULT: "hsl(var(--muted))",
        },
        subtitle: {
          DEFAULT: "hsl(var(--subtitle))",
        },
        accent: {
          DEFAULT: "hsl(var(--accent))",
        }
      },
      borderRadius: {
        lg: `var(--radius)`,
        md: `calc(var(--radius) - 2px)`,
        sm: "calc(var(--radius) - 4px)",
      },
      keyframes: {
        "accordion-down": {
          from: { height: "0" },
          to: { height: "var(--radix-accordion-content-height)" },
        },
        "accordion-up": {
          from: { height: "var(--radix-accordion-content-height)" },
          to: { height: "0" },
        },
      },
      animation: {
        "accordion-down": "accordion-down 0.2s ease-out",
        "accordion-up": "accordion-up 0.2s ease-out",
      },
    },
  },
  plugins: [
    require("tailwindcss-animate")
  ],
}