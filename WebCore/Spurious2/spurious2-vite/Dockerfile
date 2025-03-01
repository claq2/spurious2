FROM node:22
WORKDIR /app
COPY package*.json ./
RUN ls
RUN npm install
COPY . .
EXPOSE 5000
CMD ["npm", "run", "dev"]
