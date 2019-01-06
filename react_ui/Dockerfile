FROM node:9.6.1

WORKDIR /ui

COPY package.json /ui/package.json

RUN npm i

COPY . /ui

RUN npm run build

CMD ["npm", "start"]