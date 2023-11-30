FROM node:16

ENV NODE_ENV development

WORKDIR /app
COPY ["package.json","package-lock.json", "yarn.lock", "./"]

RUN yarn
RUN npm i @esbuild/linux-arm64
RUN npm ci
COPY . .
EXPOSE 3000
EXPOSE 24678