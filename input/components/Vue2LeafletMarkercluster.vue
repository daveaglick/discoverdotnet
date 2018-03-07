<template>
  <div>
    <slot></slot>
  </div>
</template>

<script>
// Need to bring this in manually because latest version has bugs when using a popup
// Also, it's not setup to work outside a bundler - this lets us bypass that
module.exports = {
  props: [ 'options' ],
  watch: {
    options: function() {
      this._remove()
      this._add()
    }
  },
  mounted () {
    this._add()
  },
  beforeDestroy () {
    this._remove()
  },
  methods: {
    deferredMountedTo (parent) {
      this.parent = parent
      var that = this.mapObject
      for (var i = 0; i < this.$children.length; i++) {
        this.$children[i].deferredMountedTo(that)
      }
      this.mapObject.addTo(parent)
    },
    _remove () {
      this.parent.removeLayer(this.mapObject)
    },
    _add () {
      this.mapObject = L.markerClusterGroup(this.options)
      if (this.$parent._isMounted) {
        this.deferredMountedTo(this.$parent.mapObject)
      }
    },
  }
}
</script>