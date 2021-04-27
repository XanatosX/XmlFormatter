using System;
using System.Threading.Tasks;
using XmlFormatterModel.Update;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Factories;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// This command will execute a given update strategy
    /// </summary>
    internal class ExecuteUpdateStrategyCommand : BaseDataCommand
    {
        private readonly Predicate<IReleaseAsset> assetFilter;

        public ExecuteUpdateStrategyCommand()
        {
            UpdatePredicateFactory predicateFactory = new UpdatePredicateFactory();
            assetFilter = predicateFactory.GetFilter();
        }

        /// <summary>
        /// This method is not implemented!
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>An error if called</returns>
        public override Task AsyncExecute(object parameter)
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parameter is ExecuteApplicationUpdateData;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (parameter is ExecuteApplicationUpdateData data)
            {
                if (data.Strategy == null)
                {
                    return;
                }
                data.Strategy.Update(data.VersionCompare, assetFilter);
            }
        }

        /// <inheritdoc/>
        public override T GetData<T>()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override bool IsExecuted()
        {
            throw new NotImplementedException();
        }
    }
}
