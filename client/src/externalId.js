const CHARSET =
  'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';

module.exports = () =>
  new Array(32)
    .fill(0)
    .map(() => CHARSET[(Math.random() * CHARSET.length) | 0])
    .join('');
