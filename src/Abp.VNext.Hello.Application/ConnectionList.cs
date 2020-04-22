﻿using Microsoft.AspNetCore.Connections;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Abp.VNext.Hello
{
    public class ConnectionList : IReadOnlyCollection<ConnectionContext>
    {
        private readonly ConcurrentDictionary<string, ConnectionContext> _connections = new ConcurrentDictionary<string, ConnectionContext>(StringComparer.Ordinal);

        public ConnectionContext this[string connectionId]
        {
            get
            {
                if (_connections.TryGetValue(connectionId, out var connection))
                {
                    return connection;
                }
                return null;
            }
        }

        public int Count => _connections.Count;

        int IReadOnlyCollection<ConnectionContext>.Count => throw new NotImplementedException();

        public void Add(string v, ConnectionContext connection)
        {
            _connections.TryAdd(connection.ConnectionId, connection);
        }

        public void Remove(ConnectionContext connection)
        {
            _connections.TryRemove(connection.ConnectionId, out var dummy);
        }

        public IEnumerator<ConnectionContext> GetEnumerator()
        {
            foreach (var item in _connections)
            {
                yield return item.Value;
            }
        }


        IEnumerator<ConnectionContext> IEnumerable<ConnectionContext>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}